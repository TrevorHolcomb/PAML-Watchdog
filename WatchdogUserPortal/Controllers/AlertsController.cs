﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace WatchdogUserPortal.Controllers
{
    public class AlertsController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived)
        {
            switch (ActiveOrArchived)
            {
                case "UnAcknowledged":
                case "Acknowledged":
                    var activeAlerts = db.Alerts.Include(a => a.AlertType).Include(a => a.Rule).Where(a => a.Status.ToString() == "UnAcknowledged" || a.Status.ToString() == "Acknowledged").OrderBy(a => a.Status);
                    return View(activeAlerts.ToList());
                default:
                    var archivedAlerts = db.Alerts.Include(a => a.AlertType).Include(a => a.Rule).Where(a => a.Status.ToString() == "Resolved").OrderBy(a => a.Status);
                    return View(archivedAlerts.ToList());
            }            
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
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name");
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Payload,Timestamp,AlertTypeId,RuleId,Notes,Status")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Alerts.Add(alert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name", alert.AlertTypeId);
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
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name", alert.AlertTypeId);
            ViewBag.RuleId = new SelectList(db.Rules, "Id", "Name", alert.RuleId);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Payload,Timestamp,AlertTypeId,RuleId,Notes,Status")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {ActiveOrArchived = alert.Status.ToString() });
            }
            ViewBag.AlertTypeId = new SelectList(db.AlertTypes, "Id", "Name", alert.AlertTypeId);
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
