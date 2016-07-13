using System;
using System.Collections.Generic;
using System.Collections;
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

        [Inject]
        public Repository<AlertGroup> AlertGroupRepository { private get; set; }

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived, int? Page_No, String sortSelect, String searchString)
        {
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            var alerts = AlertRepository.Get();
            String status;

            switch (ActiveOrArchived)
            {
                case "Resolved":
                    alerts = alerts.Where(a => a.AlertStatus.StatusCode == StatusCode.Resolved);
                    status = StatusCode.Resolved.ToString();
                    break;
                default:
                    alerts = alerts.Where(a => a.AlertStatus.StatusCode == StatusCode.Acknowledged || a.AlertStatus.StatusCode == StatusCode.UnAcknowledged);
                    status = StatusCode.Acknowledged.ToString();
                    break;
            }

            var searchStringExists = !String.IsNullOrEmpty(searchString);
            switch (sortSelect)
            {
                case "0":
                    if (searchStringExists)
                        alerts = alerts.Where(a => Convert.ToString(a.Severity).Contains(searchString));
                    alerts = alerts.OrderByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "1":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.AlertStatus.StatusCode.ToString().ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.AlertStatus.StatusCode).ThenByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "2":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.Rule.SupportCategory.Name.ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.Rule.SupportCategory.Name).ThenByDescending(a => a.Severity);
                    break;
                case "3":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.AlertType.Name.ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.AlertType.Name);
                    break;
                case "4":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.AlertStatus.TimeStamp.ToString().Contains(searchString));
                    alerts = alerts.OrderByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                case "5":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.Origin.ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.Origin).ThenByDescending(a => a.AlertStatus.TimeStamp);
                    break;
                default:
                    alerts = alerts.OrderByDescending(a => a.AlertStatus.TimeStamp).ThenByDescending(a => a.Severity);
                    break;
            }
            Hashtable checkGroup = new Hashtable();
            List<Alert> groupedAlerts = new List<Alert>();
            foreach (Alert a in alerts)
            {
                if(!checkGroup.Contains(a.AlertGroupId))
                {
                    groupedAlerts.Add(a);
                    checkGroup.Add(a.AlertGroupId, a.AlertGroupId);
                }
            }
            
            return View(new AlertPagingCreateView(groupedAlerts.ToPagedList(No_Of_Page, Size_Of_Page), status, No_Of_Page));
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int id, int PageNo, String sortOrder)
        {
            var viewModel = new AlertDetailsViewModel(AlertRepository.GetById(id), PageNo, sortOrder);
            var groupedAlerts = AlertRepository.Get();
            viewModel.Alert = AlertRepository.GetById(id);
            groupedAlerts = groupedAlerts.Where(a => a.AlertGroupId == viewModel.Alert.AlertGroupId && a.Id != viewModel.Alert.Id);
            if (viewModel.Alert == null)
            {
                return HttpNotFound();
            }
            viewModel.sortOrder = sortOrder;
            viewModel.PageNo = PageNo;
            viewModel.groupedAlerts = groupedAlerts.ToList();
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
                var editedAlert = alertViewModel.Alert;
                var groupedAlerts = AlertRepository.Get();
                groupedAlerts = groupedAlerts.Where(a => a.AlertGroupId == editedAlert.AlertGroupId);
                foreach (var alert in groupedAlerts)
                {
                    var alertInDb = AlertRepository.GetById(alert.Id);

                    if (alertInDb != null)
                    {
                        // create and insert a new AlertStatus
                        var newStatus = new AlertStatus();
                        newStatus.ModifiedBy = "N/A";
                        newStatus.StatusCode = editedAlert.AlertStatus.StatusCode;
                        newStatus.Prev = AlertStatusRepository.GetById(alertInDb.AlertStatus.Id);

                        AlertStatusRepository.Insert(newStatus);
                        AlertStatusRepository.Save();

                        newStatus = AlertStatusRepository.Get().Last();

                        var tailAlertStatus = AlertStatusRepository.GetById(alertInDb.AlertStatus.Id);
                        tailAlertStatus.Next = newStatus;
                        tailAlertStatus.Alert = null;
                        alertInDb.AlertStatus = newStatus;
                        newStatus.Alert = alertInDb;
                        AlertStatusRepository.Update(newStatus);
                        AlertStatusRepository.Save();

                        AlertStatusRepository.Update(tailAlertStatus);
                        AlertStatusRepository.Save();



                        if (alertInDb.Id == editedAlert.Id)
                        {
                            alertInDb.Severity = editedAlert.Severity;
                            alertInDb.Notes = editedAlert.Notes;
                        }
                        AlertRepository.Update(alertInDb);
                        AlertRepository.Save();
                    }
                }
                var group = AlertGroupRepository.GetById(editedAlert.AlertGroupId);
                if (editedAlert.AlertStatus.StatusCode.ToString().CompareTo("Resolved") == 0)
                {
                    group.Resolved = true;
                }
                else
                    group.Resolved = false;
                AlertGroupRepository.Update(group);
                AlertGroupRepository.Save();
                return RedirectToAction("Index", new {ActiveOrArchived = editedAlert.AlertStatus.StatusCode.ToString() });
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
