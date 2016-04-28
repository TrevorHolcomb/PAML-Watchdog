using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class AlertsController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: Alerts
        public ActionResult Index()
        {
            var alerts = db.Alerts.Include(a => a.AlertType).Include(a => a.Rule);
            return View(alerts.ToList());
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // GET: Alerts/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AlertTypes, "Id", "Name");
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AlertTypeId,RuleId,Payload,Timestamp,AlertStatusId")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Alerts.Add(alert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AlertTypes, "Id", "Name", alert.Id);
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name", alert.RuleId);
            return View(alert);
        }

        // GET: Alerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AlertTypes, "Id", "Name", alert.Id);
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name", alert.RuleId);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AlertTypeId,RuleId,Payload,Timestamp,AlertStatusId")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AlertTypes, "Id", "Name", alert.Id);
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name", alert.RuleId);
            return View(alert);
        }

        // GET: Alerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alert alert = db.Alerts.Find(id);
            db.Alerts.Remove(alert);
            db.SaveChanges();
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
