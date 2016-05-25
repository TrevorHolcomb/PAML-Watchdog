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
using AdministrationPortal.ViewModels.Alerts;
using Ninject;
using PagedList;

namespace AdministrationPortal.Controllers
{
    public class AlertsController : Controller
    {
        [Inject]
        public Repository<Alert> AlertRepository { private get; set; }

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived, int? Page_No)
        {
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            switch (ActiveOrArchived)
            {
                case "UnAcknowledged":
                case "Acknowledged":
                    var activeAlerts = AlertRepository.Get().Where(a => a.Status.ToString() == "Acknowledged" || a.Status.ToString() == "UnAcknowledged").OrderBy(a => a.Status).ThenByDescending(a => a.TimeCreated);
                    ViewBag.Active = "UnAcknowledged";
                    return View(new AlertPagingCreateView(activeAlerts.ToPagedList(No_Of_Page, Size_Of_Page), "Acknowledged"));
                default:
                    var archivedAlerts = AlertRepository.Get().Where(a => a.Status.ToString() == "Resolved").OrderByDescending(a => a.TimeCreated);
                    ViewBag.Active = "Resolved";
                    return View(new AlertPagingCreateView(archivedAlerts.ToPagedList(No_Of_Page, Size_Of_Page), "Resolved"));
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
            var viewModel = new AlertCreateViewModel(null, null);
            viewModel.Alert = alert;
            return View(viewModel);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Severity,TimeCreated,TimeModified,AlertTypeId,RuleId,Notes,Status,Server,Origin,Engine,MessageTypeId,AlertParameters,Assignee")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                Alert alertInDb = AlertRepository.GetById(alert.Id);
                if (alertInDb != null)
                {
                    alertInDb = mapNewAlertOntoDbAlert(alert);
                }
                AlertRepository.Update(alertInDb);
                AlertRepository.Save();
                return RedirectToAction("Index", new {ActiveOrArchived = alert.Status.ToString() });
            }
            return View(alert);
        }

        private Alert mapNewAlertOntoDbAlert(Alert newAlert)
        {
            Alert dbAlert = AlertRepository.GetById(newAlert.Id);
            if (dbAlert != null)
            {
                dbAlert.Status = newAlert.Status;
                dbAlert.Severity = newAlert.Severity;
                dbAlert.Notes = newAlert.Notes;
            }
            return dbAlert;
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
