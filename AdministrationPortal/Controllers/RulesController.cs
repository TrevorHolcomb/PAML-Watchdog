using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer;
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
            //ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(MessageTypeRepository.Get(), "Id", "Name");
            return View();
        }

        
        
        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RuleCreateViewModel ruleCreateViewModel)
        {
            var rule = new Rule
            {
                AlertType = AlertTypeRepository.GetById(ruleCreateViewModel.AlertTypeId),
                AlertTypeId = ruleCreateViewModel.AlertTypeId,
                Name = ruleCreateViewModel.Name,
                Description = ruleCreateViewModel.Description,
                MessageType = MessageTypeRepository.GetById(ruleCreateViewModel.MessageTypeId),
                Expression = ruleCreateViewModel.RuleTriggerSchema,
                Origin = ruleCreateViewModel.Origin,
                Server = ruleCreateViewModel.Server
            };

            RuleRepository.Insert(rule);
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

            //ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(MessageTypeRepository.Get(), "Id", "Name");
            return View("Create", rule);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RuleCategoryId,RuleTriggerSchema,EscalationChainId,AlertTypeId")] Rule rule)
        {

            if (ModelState.IsValid)
            {
                RuleRepository.Update(rule);
                RuleRepository.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(MessageTypeRepository.Get(), "Id", "Name");

            return View("Create", rule);
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
    }
}
