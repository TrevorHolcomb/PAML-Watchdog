using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class RulesController : Controller
    {
        private WatchdogDatabaseContext db = new WatchdogDatabaseContext();

        // GET: Rules
        public async Task<ActionResult> Index()
        {
            var rules = db.Rules.Include(r => r.RuleCategory);
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
            ViewBag.RuleCategoryId = new SelectList(db.RuleCategories, "Id", "Name");
            return View();
        }

        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,RuleCategoryId,RuleTrigger,EscalationChainId,AlertTypeId")] Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Rules.Add(rule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RuleCategoryId = new SelectList(db.RuleCategories, "Id", "Name", rule.RuleCategoryId);
            return View(rule);
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
            ViewBag.RuleCategoryId = new SelectList(db.RuleCategories, "Id", "Name", rule.RuleCategoryId);
            return View(rule);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RuleCategoryId,RuleTrigger,EscalationChainId,AlertTypeId")] Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RuleCategoryId = new SelectList(db.RuleCategories, "Id", "Name", rule.RuleCategoryId);
            return View(rule);
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
