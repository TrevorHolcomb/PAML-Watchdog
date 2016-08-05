using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.Alerts;
using Ninject;
using NLog;
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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: Alerts
        public ActionResult Index(string ActiveOrArchived, int? Page_No, string sortSelect, string searchString)
        {
            const int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            var alerts = AlertRepository.Get();
            string status;

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

            var searchStringExists = !string.IsNullOrEmpty(searchString);
            switch (sortSelect)
            {
                case "0":
                    if (searchStringExists)
                        alerts = alerts.Where(a => Convert.ToString(a.Severity).Contains(searchString));
                    alerts = alerts.OrderByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.Timestamp);
                    break;
                case "1":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.AlertStatus.StatusCode.ToString().ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.AlertStatus.StatusCode).ThenByDescending(a => a.Severity).ThenByDescending(a => a.AlertStatus.Timestamp);
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
                        alerts = alerts.Where(a => a.AlertStatus.Timestamp.ToString().Contains(searchString));
                    alerts = alerts.OrderByDescending(a => a.AlertStatus.Timestamp);
                    break;
                case "5":
                    if(searchStringExists)
                        alerts = alerts.Where(a => a.Origin.ToLower().Contains(searchString.ToLower()));
                    alerts = alerts.OrderBy(a => a.Origin).ThenByDescending(a => a.AlertStatus.Timestamp);
                    break;
                default:
                    alerts = alerts.OrderByDescending(a => a.AlertStatus.Timestamp).ThenByDescending(a => a.Severity);
                    break;
            }

            var checkGroup = new Hashtable();
            var checkCategory = new Hashtable();
            var groupedAlerts = new List<Alert>();

            // checks to see if you are in the archived section
            // Uses last modified to keep the category grouping once resolved
            foreach(var alert in alerts)
            {
                if (alert.AlertGroup.AlertCategoryId != null && !checkGroup.Contains(alert.AlertGroupId))
                {
                    var alertCategory = alerts.Where(a => a.AlertGroup.AlertCategoryId == alert.AlertGroup.AlertCategoryId);
                    if (ActiveOrArchived == "Resolved")
                        alertCategory = alertCategory.Where(a => a.AlertStatus.MostRecent().Timestamp.ToString("MM/dd/yy H:mm").CompareTo(alert.AlertStatus.MostRecent().Timestamp.ToString("MM/dd/yy H:mm")) == 0);
                    foreach (var alertInCategory in alertCategory)
                    {
                        if (!checkGroup.Contains(alertInCategory.AlertGroupId))
                            checkGroup.Add(alertInCategory.AlertGroupId, alertInCategory.AlertGroupId);
                    }
                    groupedAlerts.Add(alert);
                }
                else if (alert.AlertGroup.AlertCategoryId == null && !checkGroup.Contains(alert.AlertGroupId))
                {
                    groupedAlerts.Add(alert);
                    checkGroup.Add(alert.AlertGroupId, alert.AlertGroupId);
                }
                
            }
            
            return View(new AlertPagingCreateView(groupedAlerts.ToPagedList(No_Of_Page, Size_Of_Page), status, No_Of_Page));
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int id, String StatusCode, DateTime lastModified, int PageNo, String sortOrder)
        {
            var viewModel = new AlertDetailsViewModel(AlertRepository.GetById(id), PageNo, sortOrder);
            var groupedAlerts = AlertRepository.Get();
            
            
            if (viewModel.Alert == null)
                throw new ArgumentNullException(nameof(viewModel.Alert));
            if (viewModel.Alert.AlertGroup.AlertCategoryId != null)
            {
                String myTempTime = lastModified.ToString("MM/dd/yy H:mm");
                if (StatusCode == "Resolved")
                    groupedAlerts = groupedAlerts.Where(a => a.AlertGroup.AlertCategoryId == viewModel.Alert.AlertGroup.AlertCategoryId && a.Id != viewModel.Alert.Id && a.AlertStatus.Timestamp.ToString("MM/dd/yy H:mm").CompareTo(lastModified.ToString("MM/dd/yy H:mm")) == 0);
                else
                    groupedAlerts = groupedAlerts.Where(a => a.AlertGroup.AlertCategoryId == viewModel.Alert.AlertGroup.AlertCategoryId && a.Id != viewModel.Alert.Id && a.AlertStatus.StatusCode.ToString() != "Resolved");
            }
            else
                groupedAlerts = groupedAlerts.Where(a => a.AlertGroupId == viewModel.Alert.AlertGroupId && a.Id != viewModel.Alert.Id);
            
            viewModel.sortOrder = sortOrder;
            viewModel.PageNo = PageNo;
            viewModel.groupedAlerts = groupedAlerts.ToList();

            return View(viewModel);
        }

       

        // GET: Alerts/Edit/5
        public ActionResult Edit(int id, int PageNo, String sortOrder)
        {
            var alert = AlertRepository.GetById(id);
            if (alert == null)
                throw new ArgumentException($"No Alert found with Id: {id}");

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
                var oldAlertStatusHelper = AlertRepository.GetById(editedAlert.Id);
                if(oldAlertStatusHelper.AlertStatus.StatusCode.ToString() == "Resolved")
                    groupedAlerts = groupedAlerts.Where(a => a.AlertGroupId == editedAlert.AlertGroupId || (a.AlertGroup.AlertCategoryId != null && a.AlertGroup.AlertCategoryId == editedAlert.AlertGroup.AlertCategoryId && a.AlertStatus.MostRecent().Timestamp.ToString("MM/dd/yy H:mm").CompareTo(editedAlert.AlertStatus.MostRecent().Timestamp.ToString("MM/dd/yy H:mm")) == 0));
                else
                    groupedAlerts = groupedAlerts.Where(a => a.AlertGroupId == editedAlert.AlertGroupId || (a.AlertGroup.AlertCategoryId != null && a.AlertGroup.AlertCategoryId == editedAlert.AlertGroup.AlertCategoryId && a.AlertStatus.StatusCode.ToString() != "Resolved"));
                foreach (var alert in groupedAlerts)
                {
                    var alertInDb = AlertRepository.GetById(alert.Id);

                    if (alertInDb != null)
                    {
                        // create and insert a new AlertStatus
                        var newStatus = new AlertStatus()
                        {
                            ModifiedBy = "N/A",
                            StatusCode = editedAlert.AlertStatus.StatusCode,
                            Prev = AlertStatusRepository.GetById(alertInDb.AlertStatus.Id),
                            Timestamp = DateTime.Now
                        };

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
                        var group = AlertGroupRepository.GetById(alert.AlertGroupId);
                        if (editedAlert.AlertStatus.StatusCode.ToString().CompareTo("Resolved") == 0)
                        {
                            group.Resolved = true;
                        }
                        else
                            group.Resolved = false;
                        AlertGroupRepository.Update(group);
                        AlertGroupRepository.Save();
                    }
                }

                return RedirectToAction("Index", new {ActiveOrArchived = editedAlert.AlertStatus.StatusCode.ToString() });
            }
            
            return View(alertViewModel);
        }


        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = RedirectToAction("Index");
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
