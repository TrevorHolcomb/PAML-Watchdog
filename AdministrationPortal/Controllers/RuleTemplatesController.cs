using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using AdministrationPortal.ViewModels.RuleTemplates;
using Ninject;
using WatchdogDatabaseAccessLayer;
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

        // GET: RuleTemplates
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            var viewModel = new ViewRuleTemplateViewModel()
            {
                RuleTemplates = RuleTemplateRepository.Get()
            };

            var ruleTemplateUsed = RuleTemplateRepository.GetById(id);
            if (id > 0 && ruleTemplateUsed != null)
            {
                viewModel.RuleTemplateInstantiated = ruleTemplateUsed;
                viewModel.InfoMessageHidden = false;
                viewModel.NumberOfRulesInstantiated = GetLastInstantiatedRules(ruleTemplateUsed).Count();
            }
            return View(viewModel);
        }

        // GET: RuleTemplates/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id <= 0)
                RedirectToAction("Index");

            var ruleTemplate = RuleTemplateRepository.GetById(id);
            if (ruleTemplate == null)
                return HttpNotFound("No RuleTemplate found with id " + id);

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            return View(viewModel);
        }

        // GET: RuleTemplates/Create
        public ActionResult Create()
        {
            var viewModel = new CreateRuleTemplateViewModel();
            var rules = RuleRepository.Get().ToList();
            viewModel.Rules = GetUniqueRules(rules).ToList();

            return View(viewModel);
        }

        // POST: RuleTemplates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRuleTemplateViewModel viewModel)
        {
            var ruleIdsToAdd = viewModel.Rules.Where((r, index) => viewModel.RulesIncluded.ElementAt(index)).Select(r => r.Id);
            var templatedRulesToAdd = ruleIdsToAdd.Select(id => RuleRepository.GetById(id).ToTemplate());

            var ruleTemplate = new RuleTemplate()
            {
                LastUsedOn = DateTime.Now,
                Name = viewModel.Name,
                Description = viewModel.Description,
                TemplatedRules = templatedRulesToAdd.ToList()
            };

            RuleTemplateRepository.Insert(ruleTemplate);
            try
            {
                RuleTemplateRepository.Save();
            }
            catch (DbEntityValidationException deve)
            {
                throw new FormattedDbEntityValidationException(deve);
            }

            var ruleTemplateInDb = RuleTemplateRepository.Get()
                .First(rt => rt.LastUsedOn == ruleTemplate.LastUsedOn
                    && rt.Name == ruleTemplate.Name
                    && rt.Description == ruleTemplate.Description
                    && rt.TemplatedRules.Count == ruleTemplate.TemplatedRules.Count);

            return RedirectToAction("Index", ruleTemplateInDb.Id);
        }

        // GET: RuleTemplates/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {           
            if (id <= 0)
                RedirectToAction("Index");

            var ruleTemplate = RuleTemplateRepository.GetById(id);
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
            ruleTemplate.TemplatedRules.ForEach(tr => viewModel.TemplatedRulesIncluded.Add(true));

            return View(viewModel);
        }

        // POST: RuleTemplates/Edit/5
        [HttpPost]
        public ActionResult Edit(EditRuleTemplateViewModel viewModel) 
        {
            try
            {
                //Get the selected TemplatedRules
                var ruleTemplateInDb = RuleTemplateRepository.GetById(viewModel.Id);
                var templatedRulesToInclude = new List<TemplatedRule>();
                if (viewModel.TemplatedRules != null && viewModel.TemplatedRulesIncluded != null)
                    templatedRulesToInclude = viewModel.TemplatedRules
                        .Where((tr, index) => viewModel.TemplatedRulesIncluded.ElementAt(index))
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
                
                RuleTemplateRepository.Update(ruleTemplateInDb);
                RuleTemplateRepository.Save();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("Index");
            }
        }

        // GET: RuleTemplates/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return HttpNotFound("No RuleTemplate found with id " + id);

            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                return HttpNotFound("No RuleTemplate found with id " + id);

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            return View(viewModel);
        }

        // POST: RuleTemplates/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
                return HttpNotFound("No RuleTemplate found with id " + id);

            var ruleTemplate = RuleTemplateRepository.GetById(id);

            if (ruleTemplate == null)
                return HttpNotFound("No RuleTemplate found with id " + id);

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
        [HttpGet]
        public ActionResult Use(int id)
        {
            if (id <= 0)
                RedirectToAction("Index");

            var ruleTemplate = RuleTemplateRepository.GetById(id);
            if (ruleTemplate == null)
                return HttpNotFound("No RuleTemplate found with id " + id);

            var viewModel = BuildDetailsViewModel(ruleTemplate);
            viewModel.RegisteredEngines = new SelectList(EngineRepository.Get().Select(e => e.Name).ToList());

            return View(viewModel);
        }

        // POST: RuleTemplates/Use/5
        [HttpPost]
        public ActionResult Use(DetailsRuleTemplateViewModel viewModel)
        {
            viewModel.RegisteredEngines = new SelectList(EngineRepository.Get().Select(e => e.Name).ToList());
            viewModel.RuleTemplate = RuleTemplateRepository.GetById(viewModel.RuleTemplateId);

            if (!ModelState.IsValid)
                return View(viewModel);

            var originServerTuples = BuildTuples(viewModel.Origins, viewModel.Servers);
            var ruleTemplate = viewModel.RuleTemplate;
            var rules = BuildRulesFromTemplate(ref ruleTemplate, viewModel.Engine, originServerTuples);

            RuleRepository.InsertRange(rules);
            RuleRepository.Save();

            ruleTemplate.LastEngine = viewModel.Engine;
            ruleTemplate.LastOrigins = viewModel.OriginsString;
            ruleTemplate.LastServers = viewModel.ServersString;
            RuleTemplateRepository.Update(ruleTemplate);
            RuleTemplateRepository.Save();

            return RedirectToAction("Index", new { id = ruleTemplate.Id });
        }

        //AJAX method
        [HttpPost]
        public string Undo(int id)
        {
            if (id <= 0)
                return "Invalid RuleTemplate Id";

            var ruleTemplateToUndo = RuleTemplateRepository.GetById(id);
            if (ruleTemplateToUndo == null)
                return "RuleTemplate not found";

            var rulesToDelete = GetLastInstantiatedRules(ruleTemplateToUndo).ToList();

            RuleRepository.DeleteRange(rulesToDelete);
            RuleRepository.Save();
            return "" + rulesToDelete.Count + " Rules were deleted.";
        }

        #region private helper methods

        /// <summary>
        /// Interprets the forms.
        /// </summary>
        private static IEnumerable<Tuple<string, string>> BuildTuples(
            IEnumerable<string> origins, IEnumerable<string> servers)
        {
            var originsList = origins.ToList();     //to prevent multiple traversal
            var serversList = servers.ToList();
            var tuples = new List<Tuple<string, string>>();

            if (originsList.Count() == 1)
                tuples.AddRange(serversList.Select(s => Tuple.Create(originsList.First(), s)));
            else if (serversList.Count() == 1)
                tuples.AddRange(originsList.Select(o => Tuple.Create(o, serversList.First())));
            else if (originsList.Count() == serversList.Count())
                for (var i = 0; i < originsList.Count(); i++)
                    tuples.Add(Tuple.Create(originsList.ElementAt(i), serversList.ElementAt(i)));

            return tuples;
        }

        /// <summary>
        /// Builds a set of rules from all TemplatedRules in a RuleTemplate, and a collection of origin/server tuples
        /// </summary>
        private static IEnumerable<Rule> BuildRulesFromTemplate(ref RuleTemplate template,
            string engine, IEnumerable<Tuple<string, string>> originServerTuples)
        {
            var timestamp = DateTime.Now;
            template.LastUsedOn = timestamp;

            var rules = new List<Rule>();
            originServerTuples = originServerTuples.ToList();
            foreach (var rule in template.TemplatedRules)
                rules.AddRange(BuildRuleFromTemplate(rule, engine, originServerTuples, timestamp));

            return rules;
        }

        /// <summary>
        /// Build a set of Rules from a single TemplatedRule and collection of origin/server tuples
        /// </summary>
        private static IEnumerable<Rule> BuildRuleFromTemplate(TemplatedRule template,
            string engine, IEnumerable<Tuple<string, string>> originServerTuples, DateTime timestamp)
        {
            var rules = new List<Rule>();
            foreach (var tuple in originServerTuples)
                rules.Add(new Rule
                {
                    Engine = engine,
                    Origin = tuple.Item1,
                    Server = tuple.Item2,
                    AlertTypeId = template.AlertTypeId,
                    DefaultSeverity = template.DefaultSeverity,
                    Description = template.Description,
                    Expression = template.Expression,
                    MessageTypeName = template.MessageTypeName,
                    Name = template.Name,
                    RuleCreator = template.RuleCreator,                         //TODO: set to creator of template?
                    SupportCategoryId = template.SupportCategoryId,
                    Timestamp = timestamp
                });
            return rules;
        }

        private DetailsRuleTemplateViewModel BuildDetailsViewModel(RuleTemplate ruleTemplate)
        {
            var orderedTemplatedRules = ruleTemplate.TemplatedRules.OrderBy(rt => rt.Id);
            var viewModel = new DetailsRuleTemplateViewModel()
            {
                RuleTemplate = ruleTemplate,
                RuleTemplateId = ruleTemplate.Id,
                RuleAlertTypeNames = orderedTemplatedRules
                    .Select(tr => AlertTypeRepository.GetById(tr.AlertTypeId).Name),
                RuleRuleCategoryNames = orderedTemplatedRules.Select(tr => tr.Name)
            };
            return viewModel;
        }

        /// <summary>
        /// Finds all rules with the same timestamp and last engine, origins, and servers used by the template
        /// </summary>
        private IEnumerable<Rule> GetLastInstantiatedRules(RuleTemplate toMatch)
        {
            var origins = toMatch.LastOrigins.Split(',')
                .Select(o => o.Trim()).Where(o => !o.IsEmpty());
            var servers = toMatch.LastServers.Split(',')
                .Select(s => s.Trim()).Where(s => !s.IsEmpty());

            var originServerTuples = BuildTuples(origins, servers);

            return RuleRepository.Get()
                .Where(r => r.Timestamp == toMatch.LastUsedOn)
                .Where(r => r.Engine == toMatch.LastEngine)
                .Where(r => originServerTuples.First(ost => ost.Item1 == r.Origin && ost.Item2 == r.Server) != null);
        }

        /// <summary>
        /// TODO: O(n^2), improve with hashmap
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