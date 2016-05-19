using System.Linq;
using System.Web.Mvc;
using Ninject;
using AdministrationPortal.ViewModels;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class RulesController : Controller
    {
        [Inject]
        public Repository<Rule> RuleRepository { get; set; }
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { get; set; }
        [Inject]
        public Repository<AlertType> AlertTypeRepository { get; set; }
        [Inject]
        public Repository<MessageType> MessageTypeRepository { get; set; }
        [Inject]
        public Repository<EscalationChain> EscalationChainRepository { get; set; }
        [Inject]
        public Repository<SupportCategory> SupportCategoryRepository { get; set; }

        // GET: Rules
        public ActionResult Index()
        {
            var rules = RuleRepository.Get();
            return View(rules);
        }

        // GET: Rules/Details/5
        public ActionResult Details(int id)
        {
            var rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // GET: Rules/Create
        public ActionResult Create()
        {
            //intialize the dropdowns
            RuleViewModels ruleViewModel = new RuleViewModels();
            ruleViewModel.RuleOptions = new RuleOptionsViewModel()
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EscalationChains = new SelectList(EscalationChainRepository.Get(), "Id", "Name"),
                MessageTypes = new SelectList(MessageTypeRepository.Get(), "Id", "Name"),
                RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name")
            };

            //sets default values
            ruleViewModel.RuleToCreate = new Rule();

            return View(ruleViewModel);
        }

        
        
        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RuleViewModels ruleViewModel)
        {
            RuleRepository.Insert(ruleViewModel.RuleToCreate);
            RuleRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(int id)
        {
            Rule rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound();
            }

            RuleViewModels ruleViewModel = new RuleViewModels();
            ruleViewModel.RuleToCreate = rule;
            ruleViewModel.RuleOptions = new RuleOptionsViewModel
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EscalationChains = new SelectList(EscalationChainRepository.Get(), "Id", "Name"),
                MessageTypes = new SelectList(MessageTypeRepository.Get(), "Id", "Name"),
                RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name")
            };

            return View("Create", ruleViewModel);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RuleToCreate")] RuleViewModels ruleViewModel)
        {
            
            if (ModelState.IsValid) 
            {
                Rule ruleInDb = mapNewRuleOntoDbRule(ruleViewModel.RuleToCreate);

                if (ruleInDb == null)
                {
                    return HttpNotFound();
                }

                RuleRepository.Update(ruleInDb);
                RuleRepository.Save();
                return RedirectToAction("Index");
            }

            ruleViewModel.RuleOptions = new RuleOptionsViewModel
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EscalationChains = new SelectList(EscalationChainRepository.Get(), "Id", "Name"),
                MessageTypes = new SelectList(MessageTypeRepository.Get(), "Id", "Name"),
                RuleCategories = new SelectList(RuleCategoryRepository.Get(), "Id", "Name"),
                SupportCategories = new SelectList(SupportCategoryRepository.Get(), "Id", "Name")
            };

            return View("Create", ruleViewModel);
        }

        // GET: Rules/Delete/5
        public ActionResult Delete(int id)
        {
            Rule rule = RuleRepository.GetById(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rule rule = RuleRepository.GetById(id);
            RuleRepository.Delete(rule);
            RuleRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RuleRepository.Dispose();
                RuleCategoryRepository.Dispose();
                AlertTypeRepository.Dispose();
                MessageTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        //TODO: Think about which of these properties we want to allow the user to edit, and under what conditons.
        //also: change the Edit page to reflect those restrictions.
        private Rule mapNewRuleOntoDbRule(Rule newRule)
        {
            Rule dbRule = RuleRepository.GetById(newRule.Id);
            if (dbRule != null)
            {
                dbRule.AlertTypeId = newRule.AlertTypeId;
                dbRule.DefaultSeverity = newRule.DefaultSeverity;
                dbRule.Description = newRule.Description;
                dbRule.Engine = newRule.Engine;
                dbRule.EscalationChainId = newRule.EscalationChainId;
                dbRule.Expression = newRule.Expression;
                dbRule.MessageTypeId = newRule.MessageTypeId;
                dbRule.Name = newRule.Name;
                dbRule.Origin = newRule.Origin;
                dbRule.RuleCategoryId = newRule.RuleCategoryId;
                dbRule.Server = newRule.Server;
                dbRule.SupportCategoryId = newRule.SupportCategoryId;
            }
            return dbRule;
        }
    }
}
