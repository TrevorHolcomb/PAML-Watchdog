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
    public class AlertTypesController : Controller
    {
        private WatchdogDatabaseContext db = new WatchdogDatabaseContext();

        // GET: AlertTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.AlertTypes.ToListAsync());
        }

        // GET: AlertTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlertType alertType = await db.AlertTypes.FindAsync(id);
            if (alertType == null)
            {
                return HttpNotFound();
            }
            return View(alertType);
        }

        // GET: AlertTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlertTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] AlertType alertType)
        {
            if (ModelState.IsValid)
            {
                db.AlertTypes.Add(alertType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(alertType);
        }

        // GET: AlertTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlertType alertType = await db.AlertTypes.FindAsync(id);
            if (alertType == null)
            {
                return HttpNotFound();
            }
            return View(alertType);
        }

        // POST: AlertTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] AlertType alertType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alertType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(alertType);
        }

        // GET: AlertTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlertType alertType = await db.AlertTypes.FindAsync(id);
            if (alertType == null)
            {
                return HttpNotFound();
            }
            return View(alertType);
        }

        // POST: AlertTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AlertType alertType = await db.AlertTypes.FindAsync(id);
            db.AlertTypes.Remove(alertType);
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
