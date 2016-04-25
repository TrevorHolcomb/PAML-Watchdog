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
    public class NotifyeeGroupsController : Controller
    {
        private WatchdogDatabaseContext db = new WatchdogDatabaseContext();

        // GET: NotifyeeGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.NotifyeeGroups.ToListAsync());
        }

        // GET: NotifyeeGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyeeGroup notifyeeGroup = await db.NotifyeeGroups.FindAsync(id);
            if (notifyeeGroup == null)
            {
                return HttpNotFound();
            }
            return View(notifyeeGroup);
        }

        // GET: NotifyeeGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotifyeeGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Decsription")] NotifyeeGroup notifyeeGroup)
        {
            if (ModelState.IsValid)
            {
                db.NotifyeeGroups.Add(notifyeeGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(notifyeeGroup);
        }

        // GET: NotifyeeGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyeeGroup notifyeeGroup = await db.NotifyeeGroups.FindAsync(id);
            if (notifyeeGroup == null)
            {
                return HttpNotFound();
            }
            return View(notifyeeGroup);
        }

        // POST: NotifyeeGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Decsription")] NotifyeeGroup notifyeeGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notifyeeGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(notifyeeGroup);
        }

        // GET: NotifyeeGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyeeGroup notifyeeGroup = await db.NotifyeeGroups.FindAsync(id);
            if (notifyeeGroup == null)
            {
                return HttpNotFound();
            }
            return View(notifyeeGroup);
        }

        // POST: NotifyeeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NotifyeeGroup notifyeeGroup = await db.NotifyeeGroups.FindAsync(id);
            db.NotifyeeGroups.Remove(notifyeeGroup);
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
