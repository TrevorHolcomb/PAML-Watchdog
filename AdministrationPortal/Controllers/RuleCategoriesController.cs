using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class RuleCategoriesController : Controller
    {
        private readonly WatchdogDatabaseContext _db = new WatchdogDatabaseContext();

        // GET: RuleCategories
        public async Task<ActionResult> Index()
        {
            return View(await _db.RuleCategories.ToListAsync());
        }

        // GET: RuleCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RuleCategory ruleCategory = await _db.RuleCategories.FindAsync(id);
            if (ruleCategory == null)
            {
                return HttpNotFound();
            }
            return View(ruleCategory);
        }

        // GET: RuleCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RuleCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] RuleCategory ruleCategory)
        {
            if (ModelState.IsValid)
            {
                _db.RuleCategories.Add(ruleCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ruleCategory);
        }

        // GET: RuleCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RuleCategory ruleCategory = await _db.RuleCategories.FindAsync(id);
            if (ruleCategory == null)
            {
                return HttpNotFound();
            }
            return View(ruleCategory);
        }

        // POST: RuleCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] RuleCategory ruleCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(ruleCategory).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ruleCategory);
        }

        // GET: RuleCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RuleCategory ruleCategory = await _db.RuleCategories.FindAsync(id);
            if (ruleCategory == null)
            {
                return HttpNotFound();
            }
            return View(ruleCategory);
        }

        // POST: RuleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RuleCategory ruleCategory = await _db.RuleCategories.FindAsync(id);
            _db.RuleCategories.Remove(ruleCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
