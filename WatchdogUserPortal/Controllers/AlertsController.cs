using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogUserPortal.ViewModels.Alerts;
using Ninject;

namespace WatchdogUserPortal.Controllers
{
    public class AlertsController : Controller
    {
        [Inject]
        public Repository<Alert> AlertRepository { private get; set; }

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived)
        {
            
            switch (ActiveOrArchived)
            {
                case "UnAcknowledged":
                case "Acknowledged":
                    var activeAlerts = AlertRepository.Get().Where(a => a.Status.ToString() == "Acknowledged" || a.Status.ToString() == "UnAcknowledged");
                    return View(activeAlerts);
                default:
                    var archivedAlerts = AlertRepository.Get().Where(a => a.Status.ToString() == "Resolved");
                    return View(archivedAlerts);
            }         
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int id)
        {
            var alert = AlertRepository.GetById(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // GET: Alerts/Create
        public ActionResult Create()
        {
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
                AlertRepository.Insert(alert);
                AlertRepository.Save();
                return RedirectToAction("Index");
            }
            return View(alert);
        }

        // GET: Alerts/Edit/5
        public ActionResult Edit(int id)
        {
            var alert = AlertRepository.GetById(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(new AlertCreateViewModel(alert));
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
                AlertRepository.Update(alert);
                AlertRepository.Save();
                return RedirectToAction("Index", new {ActiveOrArchived = alert.Status.ToString() });
            }
            return View(alert);
        }

        // GET: Alerts/Delete/5
        public ActionResult Delete(int id)
        {
            var alert = AlertRepository.GetById(id);
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
            var alert = AlertRepository.GetById(id);
            AlertRepository.Delete(alert);
            AlertRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AlertRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
