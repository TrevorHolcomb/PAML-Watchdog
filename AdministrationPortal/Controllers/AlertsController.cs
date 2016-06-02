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

        [Inject]
        public Repository<AlertStatus> AlertStatusRepository { private get; set; }

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived, int? Page_No, String sortOrder)
        {
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            var alerts = AlertRepository.Get();
            String status;

            switch (ActiveOrArchived)
            {
                case "Resolved":
                    alerts = alerts.Where(a => a.AlertStatus.MostRecent().StatusCode == StatusCode.Resolved);
                    status = StatusCode.Resolved.ToString();
                    break;
                default:
                    alerts = alerts.Where(a => a.AlertStatus.MostRecent().StatusCode == StatusCode.Acknowledged || a.AlertStatus.MostRecent().StatusCode == StatusCode.UnAcknowledged);
                    status = StatusCode.Acknowledged.ToString();
                    break;
            }

            switch(sortOrder)
            {
                case "Severity":
                    alerts = alerts.OrderByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "Time":
                    alerts = alerts.OrderByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "Origin":
                    alerts = alerts.OrderBy(a => a.Origin).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "AlertType":
                    alerts = alerts.OrderBy(a => a.AlertType.Name);
                    break;
                case "Group":
                    alerts = alerts.OrderBy(a => a.Rule.SupportCategory.Name).ThenByDescending(a => a.Severity);
                    break;
                default:
                    alerts = alerts.OrderBy(a => a.AlertStatus.TimeStamp).ThenByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
            }

            return View(new AlertPagingCreateView(alerts.ToPagedList(No_Of_Page, Size_Of_Page), status, sortOrder, No_Of_Page));
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int id, int PageNo, String sortOrder)
        {
            var viewModel = new AlertDetailsViewModel(AlertRepository.GetById(id), PageNo, sortOrder);
            viewModel.Alert = AlertRepository.GetById(id);
            if (viewModel.Alert == null)
            {
                return HttpNotFound();
            }
            viewModel.sortOrder = sortOrder;
            viewModel.PageNo = PageNo;
            return View(viewModel);
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
        public ActionResult Edit(int id, int PageNo, String sortOrder)
        {
            var alert = AlertRepository.GetById(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AlertDetailsViewModel(alert, PageNo, sortOrder);
            return View(viewModel);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlertDetailsViewModel alertViewModel)
        {
            if (ModelState.IsValid)
            {
                var alert = alertViewModel.Alert;
                var alertInDb = AlertRepository.GetById(alert.Id);

                if (alertInDb != null)
                {
                    // create and insert a new AlertStatus
                    var newStatus = new AlertStatus();
                    newStatus.ModifiedBy = "N/A";
                    newStatus.StatusCode = alertViewModel.Alert.AlertStatus.StatusCode;
                    newStatus.Prev = alertInDb.AlertStatus;
                   

                    AlertStatusRepository.Insert(newStatus);
                    AlertStatusRepository.Save();

                    newStatus = AlertStatusRepository.Get().Last();
                    alertInDb.AlertStatus.Next = newStatus;
                    var tailAlertStatus = alertInDb.AlertStatus;
                    alertInDb.AlertStatus = newStatus;
                    
                    //newStatus.Prev = tailAlertStatus;
                   // tailAlertStatus.Next = newStatus;
                    //AlertStatusRepository.Update(newStatus);
                    AlertStatusRepository.Update(tailAlertStatus);
                    AlertStatusRepository.Save();
                    
                   
                    alertInDb.Severity = alertViewModel.Alert.Severity;
                    alertInDb.Notes = alertViewModel.Alert.Notes;
                }
                AlertRepository.Update(alertInDb);
                AlertRepository.Save();
                return RedirectToAction("Index", new {ActiveOrArchived = alert.AlertStatus.StatusCode.ToString() });
            }

            return View(alertViewModel);
        }

        private Alert mapNewAlertOntoDbAlert(Alert newAlert)
        {
            Alert dbAlert = AlertRepository.GetById(newAlert.Id);
            
            if (dbAlert != null)
            {
                AlertStatus newStatus = new AlertStatus();
                newStatus.ModifiedBy = "N/A";
                
                
                //dbAlert.AlertStatus = newAlert.AlertStatus;
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
