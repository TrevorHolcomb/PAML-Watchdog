using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class NotifyeesController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: Notifyees
        public ActionResult Index()
        {
            return View(new NotifyeesIndexViewModel
            {
                NotifyeeGroups = db.NotifyeeGroups.ToList(),
                Notifyees = db.Notifyees.ToList()
            });
        }

        // GET: Notifyees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifyee notifyee = await db.Notifyees.FindAsync(id);
            if (notifyee == null)
            {
                return HttpNotFound();
            }
            return View(notifyee);
        }

        // GET: Notifyees/Create
        public ActionResult Create()
        {
            ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name");
            return View();
        }

        // POST: Notifyees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,NotifyeeGroupId")] Notifyee notifyee)
        {
            if (ModelState.IsValid)
            {
                db.Notifyees.Add(notifyee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(notifyee);
        }

        // GET: Notifyees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifyee notifyee = await db.Notifyees.FindAsync(id);
            if (notifyee == null)
            {
                return HttpNotFound();
            }
            return View(notifyee);
        }

        // GET: Notifyees/Edit/5
        public async Task<ActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifyee notifyee = await db.Notifyees.FindAsync(id);
            if (notifyee == null)
            {
                return HttpNotFound();
            }
            return View(notifyee);
        }

        // POST: Notifyees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email")] Notifyee notifyee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notifyee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(notifyee);
        }

        // GET: Notifyees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifyee notifyee = await db.Notifyees.FindAsync(id);
            if (notifyee == null)
            {
                return HttpNotFound();
            }
            return View(notifyee);
        }

        // POST: Notifyees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Notifyee notifyee = await db.Notifyees.FindAsync(id);
            db.Notifyees.Remove(notifyee);
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
