using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class RulesController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: Rules
        public async Task<ActionResult> Index()
        {
            var rules = db.Rules.Include(r => r.RuleCategories);
            return View(await rules.ToListAsync());
        }

        // GET: Rules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rule rule = await db.Rules.FindAsync(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // GET: Rules/Create
        public ActionResult Create()
        {
            ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(db.MessageTypes, "Id", "Name");
            return View();
        }

        
        
        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public class RuleCreateViewModel
        {
            public int AlertTypeId { get; set; }
            public int MessageTypeId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int EscalationChainId { get; set; }
            public string RuleTriggerSchema { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RuleCreateViewModel ruleCreateViewModel)
        {
            var rule = new Rule
            {
                AlertType = db.AlertTypes.Single(e => e.Id == ruleCreateViewModel.AlertTypeId),
                AlertTypeId = ruleCreateViewModel.AlertTypeId,
                Name = ruleCreateViewModel.Name,
                Description = ruleCreateViewModel.Description,
                MessageType = db.MessageTypes.Single(e => e.Id == ruleCreateViewModel.MessageTypeId),
                EscalationChain = db.EscalationChains.Single(e => e.Id == ruleCreateViewModel.EscalationChainId),
                RuleTriggerSchema = ruleCreateViewModel.RuleTriggerSchema,
            };

            db.Rules.Add(rule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rule rule = await db.Rules.FindAsync(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(db.MessageTypes, "Id", "Name");
            return View("Create", rule);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RuleCategoryId,RuleTriggerSchema,EscalationChainId,AlertTypeId")] Rule rule)
        {

            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EscalationChainId = new SelectList(db.EscalationChains, "Id", "Name");
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name");
            ViewBag.MessageTypeId = new SelectList(db.MessageTypes, "Id", "Name");

            return View("Create", rule);
        }

        // GET: Rules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rule rule = await db.Rules.FindAsync(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rule rule = await db.Rules.FindAsync(id);
            db.Rules.Remove(rule);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
