using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AdministrationPortal.ViewModels.RuleTemplates;
using Ninject;
using NLog;
using AdministrationPortal.Extensions;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WebGrease.Css.Extensions;

namespace AdministrationPortal.Controllers
{
    public class RuleTemplatesController : Controller
    {
        [Inject]
        public Repository<Rule> RuleRepository { get; set; }
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { get; set; }
        [Inject]
        public Repository<RuleTemplate> RuleTemplateRepository { get; set; }
        [Inject]
        public Repository<TemplatedRule> TemplatedRuleRepository { get; set; }
        [Inject]
        public Repository<AlertType> AlertTypeRepository { get; set; }
        [Inject]
        public Repository<Engine> EngineRepository { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: RuleTemplates
        public ActionResult Index(int id = 0, string engine="", string origins="", string servers="", string timestamp="")
        {

            var viewModel = new ViewRuleTemplateViewModel()
            {
                RuleTemplates = RuleTemplateRepository.Get()
            };

            if(id < 0 )
                throw new ArgumentException($"Invalid id: {id}");

            if (id != 0)
            {

                var ruleTemplateUsed = RuleTemplateRepository.GetById(id);
                if (ruleTemplateUsed == null)
                    throw new ArgumentException($"No RuleTemplate found with Id: {id}");

                if (engine == "")
                    throw new ArgumentNullException(nameof(engine));
                if (origins == "")
                    throw new ArgumentNullException(nameof(origins));
                if (servers == "")
                    throw new ArgumentNullException(nameof(servers));
                if (timestamp == "")
                    throw new ArgumentNullException(nameof(timestamp));

                var numRulesUsed = RuleRepository.Get()
                    .Count(r => ConcatTimeEquals(ConcatTime(r.Timestamp), timestamp));

                viewModel.RuleTemplateInstantiated = ruleTemplateUsed;
                viewModel.InfoMessageHidden = false;
                viewModel.NumberOfRulesInstantiated = numRulesUsed;
                viewModel.EngineUsed = engine;
                viewModel.OriginsUsed = origins;
                viewModel.ServersUsed = servers;
                viewModel.Timestamp = timestamp;
            }

            return View(viewModel);
        }

        // GET: RuleTemplates/Details/5
        public ActionResult Details(int id)
        {
            var ruleTemplate = RuleTemplateRepository.GetById(id);
            if (ruleTemplate == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {id}");

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            return View(viewModel);
        }

        // GET: RuleTemplates/Create
        public ActionResult Create()
        {
            var rules = RuleRepository.Get().ToList();
            var viewModel = new CreateRuleTemplateViewModel()
            {
                Rules = GetUniqueRules(rules).ToList(),
                Engines = rules.Select(r => r.Engine).GetUnique().ToCSV(),
                Origins = rules.Select(r => r.Origin).GetUnique().ToCSV(),
                Servers = rules.Select(r => r.Server).GetUnique().ToCSV()
            };

            return View(viewModel);
        }

        // POST: RuleTemplates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRuleTemplateViewModel viewModel)
        {
            var ruleIdsToAdd = viewModel.Rules
                .Where((r, index) => viewModel.RulesIncluded.ElementAt(index))
                .Select(r => r.Id);

            var templatedRulesToAdd = ruleIdsToAdd
                .Select(id => RuleRepository.GetById(id).ToTemplate());

            var ruleTemplate = new RuleTemplate()
            {
                LastUsedOn = DateTime.Now,
                Name = viewModel.Name,
                Description = viewModel.Description,
                TemplatedRules = templatedRulesToAdd.ToList()
            };

            RuleTemplateRepository.Insert(ruleTemplate);
            RuleTemplateRepository.Save();

            var ruleTemplateInDb = RuleTemplateRepository.Get()
                .First(rt => rt.LastUsedOn == ruleTemplate.LastUsedOn
                    && rt.Name == ruleTemplate.Name
                    && rt.Description == ruleTemplate.Description
                    && rt.TemplatedRules.Count == ruleTemplate.TemplatedRules.Count);

            return RedirectToAction("Index", ruleTemplateInDb.Id);
        }

        // GET: RuleTemplates/Edit/5
        public ActionResult Edit(int id)
        {           
            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {id}");

            var templatedRules = ruleTemplate.TemplatedRules.ToList();

            var rules = GetUniqueRules(RuleRepository.Get())
                .Where(r => !templatedRules.Any(r.EqualsTemplatedRule)).ToList();

            var viewModel = new EditRuleTemplateViewModel()
            {
                Name = ruleTemplate.Name,
                Description = ruleTemplate.Description,
                Rules = rules,
                RulesIncluded = new List<bool>(),

                Id = id,
                TemplatedRules = templatedRules,
                TemplatedRulesIncluded = new List<bool>()
            };

            rules.ForEach(r => viewModel.RulesIncluded.Add(false));
            ruleTemplate.TemplatedRules
                .ForEach(tr => viewModel.TemplatedRulesIncluded.Add(true));

            return View(viewModel);
        }

        // POST: RuleTemplates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRuleTemplateViewModel viewModel) 
        {
            //Get the selected TemplatedRules
            var ruleTemplateInDb = RuleTemplateRepository.GetById(viewModel.Id);

            if (ruleTemplateInDb == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {viewModel.Id}");

            var templatedRulesToInclude = viewModel.TemplatedRules
                .Where((tr, index) => viewModel.TemplatedRulesIncluded.ElementAt(index))
                .Select(tr => TemplatedRuleRepository.GetById(tr.Id)).ToList();

            var templatedRulesToExclude = viewModel.TemplatedRules
                .Where((tr, index) => !viewModel.TemplatedRulesIncluded.ElementAt(index))
                .Select(tr => TemplatedRuleRepository.GetById(tr.Id)).ToList();

            //Get the selected Rules
            var rulesToInclude = new List<Rule>();
            if (viewModel.Rules != null && viewModel.RulesIncluded != null)
                rulesToInclude = viewModel.Rules
                    .Where((r, index) => viewModel.RulesIncluded.ElementAt(index))
                    .Select(r => RuleRepository.GetById(r.Id)).ToList();

            //Convert selected rules into TemplatedRules
            templatedRulesToInclude.AddRange(rulesToInclude.Select(r => r.ToTemplate()));

            ruleTemplateInDb.Name = viewModel.Name;
            ruleTemplateInDb.Description = viewModel.Description;
            ruleTemplateInDb.TemplatedRules.Clear();
            ruleTemplateInDb.TemplatedRules = templatedRulesToInclude;

            TemplatedRuleRepository.DeleteRange(templatedRulesToExclude);
            TemplatedRuleRepository.Save();
                
            RuleTemplateRepository.Update(ruleTemplateInDb);
            RuleTemplateRepository.Save();

            return RedirectToAction("Index");
            
        }

        // GET: RuleTemplates/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {id}");

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            return View(viewModel);
        }

        // POST: RuleTemplates/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {id}");

            var templatedRulesIds = ruleTemplate.TemplatedRules.Select(tr => tr.Id);

            var templatedRulesInDb =
                TemplatedRuleRepository.Get().Where(tr => templatedRulesIds.Contains(tr.Id));

            TemplatedRuleRepository.DeleteRange(templatedRulesInDb);
            TemplatedRuleRepository.Save();
            RuleTemplateRepository.Delete(ruleTemplate);
            RuleTemplateRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: RuleTemplates/Use/5
        public ActionResult Use(int id)
        {
            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                throw new ArgumentException($"No RuleTemplate found with Id: {id}");

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            viewModel.RegisteredEngines = new SelectList(EngineRepository.Get().Select(e => e.Name).ToList());
            viewModel.OriginServerTuples = new List<KeyValuePair<string, string>>();
            viewModel.TemplateInstantiator = viewModel.RuleTemplate.Name;

            return View(viewModel);
        }

        // POST: RuleTemplates/Use/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Use(UseRuleTemplateViewModel viewModel)
        {
            viewModel.RegisteredEngines = new SelectList(EngineRepository.Get().Select(e => e.Name).ToList());
            viewModel.RuleTemplate = RuleTemplateRepository.GetById(viewModel.RuleTemplateId);

            if (!ModelState.IsValid)
                return View(viewModel);

            var ruleTemplate = viewModel.RuleTemplate;
            var rules = BuildRulesFromTemplate(ref ruleTemplate, viewModel.Engine, viewModel.OriginServerTuples, viewModel.TemplateInstantiator);

            RuleRepository.InsertRange(rules);
            RuleRepository.Save();

            RuleTemplateRepository.Update(ruleTemplate);
            RuleTemplateRepository.Save();

            var lastOrigins = viewModel.OriginServerTuples.Select(kvp => kvp.Key).ToCSV();
            var lastServers = viewModel.OriginServerTuples.Select(kvp => kvp.Value).ToCSV();

            var dictionary = new
            {
                id = ruleTemplate.Id,
                engine = viewModel.Engine,
                origins = lastOrigins,
                servers = lastServers,
                timestamp = ConcatTime(ruleTemplate.LastUsedOn)
            };

            return RedirectToAction("Index", dictionary);
        }

        //AJAX method
        [HttpPost]
        public string Undo(int id, string timestamp)
        {
            if (id <= 0)
                return "Invalid RuleTemplate Id";

            var ruleTemplateToUndo = RuleTemplateRepository.GetById(id);
            if (ruleTemplateToUndo == null)
                return "RuleTemplate not found";

            var rulesToDelete = RuleRepository.Get()
                .Where(r => ConcatTimeEquals(ConcatTime(r.Timestamp), timestamp))
                .ToList();

            if (!rulesToDelete.Any())
                return "Undo was unsuccessful";

            RuleRepository.DeleteRange(rulesToDelete);
            RuleRepository.Save();
            return "" + rulesToDelete.Count() + " Rules were deleted.";
        }


        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;
                       
            filterContext.Result = RedirectToAction("Index", new { id=0 });
        }

        #region private helper methods

        /// <summary>
        /// Builds a set of rules from all TemplatedRules in a RuleTemplate, and a collection of origin/server tuples
        /// </summary>
        private static IEnumerable<Rule> BuildRulesFromTemplate(
            ref RuleTemplate template,
            string engine, 
            IEnumerable<KeyValuePair<string, string>> originServerTuples,
            string ruleCreator
            )
        {
            var timestamp = DateTime.Now;
            template.LastUsedOn = timestamp;

            var rules = new List<Rule>();
            originServerTuples = originServerTuples.ToList();
            foreach (var rule in template.TemplatedRules)
                rules.AddRange(BuildRuleFromTemplate(rule, engine, originServerTuples, timestamp, ruleCreator));

            return rules;
        }

        /// <summary>
        /// Build a set of Rules from a single TemplatedRule and collection of origin/server tuples
        /// </summary>
        private static IEnumerable<Rule> BuildRuleFromTemplate(
            TemplatedRule template,
            string engine, 
            IEnumerable<KeyValuePair<string, string>> originServerTuples, 
            DateTime timestamp,
            string ruleCreator)
        {
            var rules = new List<Rule>();
            foreach (var tuple in originServerTuples)
                rules.Add(new Rule
                {
                    Engine = engine,
                    Origin = tuple.Key,
                    Server = tuple.Value,
                    AlertTypeId = template.AlertTypeId,
                    DefaultSeverity = template.DefaultSeverity,
                    Description = template.Description,
                    Expression = template.Expression,
                    MessageTypeName = template.MessageTypeName,
                    Name = template.Name,
                    RuleCreator = ruleCreator.Trim(),
                    SupportCategoryId = template.SupportCategoryId,
                    Timestamp = timestamp
                });
            return rules;
        }

        private UseRuleTemplateViewModel BuildDetailsViewModel(RuleTemplate ruleTemplate)
        {
            var alertIdNameMap = new Dictionary<int, string>();
            ruleTemplate.TemplatedRules.ForEach(tr => alertIdNameMap[tr.AlertTypeId] = AlertTypeRepository.GetById(tr.AlertTypeId).Name);

            var ruleIdToRuleCategoryNames = ruleTemplate.TemplatedRules.ToDictionary(
                tr => tr.Id,
                tr => tr.RuleCategories.Select(rc => RuleCategoryRepository.GetById(rc.Id).Name).ToCSV()
                );

            var viewModel = new UseRuleTemplateViewModel()
            {
                RuleTemplate = ruleTemplate,
                RuleTemplateId = ruleTemplate.Id,
                AlertTypeIdToName = alertIdNameMap,
                RuleIdToRuleCategoryNames = ruleIdToRuleCategoryNames
            };
            return viewModel;
        }

        /// <summary>
        /// for passing datetimes as strings
        /// </summary>
        private static string ConcatTime(DateTime dateTime)
        {
            return "" + dateTime.Day + dateTime.Hour + dateTime.Minute + dateTime.Second + dateTime.Millisecond;
        }

        /// <summary>
        /// for comparing products of ConcatTime
        /// </summary>
        private static bool ConcatTimeEquals(string a, string b)
        {
            var timeA = Int64.Parse(a);
            var timeB = Int64.Parse(b);
            int fiveMilliseconds = 5;

            return Math.Abs(timeA - timeB) < fiveMilliseconds;
        }

        /// <summary>
        /// Used to filter Rules of an arbitrary "sameness" defined by the Rule 
        /// extension method EqualsRule
        /// </summary>
        private static IEnumerable<Rule> GetUniqueRules(IEnumerable<Rule> rules)
        {
            var uniqueRules = new List<Rule>();
            foreach (var rule in rules)
            {
                if (!uniqueRules.Any(rule.EqualsRule))
                    uniqueRules.Add(rule);
            }
            return uniqueRules;
        }

    }
    #endregion
}