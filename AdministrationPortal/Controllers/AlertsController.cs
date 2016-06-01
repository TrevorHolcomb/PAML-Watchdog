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
        public ActionResult Index(string ActiveOrArchived, int? Page_No, int? sortOrder)
        {
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            var alerts = AlertRepository.Get();
            String status;

            switch (ActiveOrArchived)
            {
                case "Resolved":
                    alerts = alerts.Where(a => a.Status.ToString() == "Resolved");
                    status = "Resolved";
                    break;
                default:
                    alerts = alerts.Where(a => a.Status.ToString() == "Acknowledged" || a.Status.ToString() == "UnAcknowledged");
                    status = "Acknowledged";
                    break;
            }

            switch(sortOrder)
            {
                case 0:
                    alerts = alerts.OrderByDescending(a => a.Severity).ThenByDescending(a => a.TimeCreated);
                    break;
                case 1:
                    alerts = alerts.OrderBy(a => a.Status).ThenByDescending(a => a.TimeCreated);
                    break;
                case 2:
                    alerts = alerts.OrderBy(a => a.Rule.SupportCategory.Name).ThenByDescending(a => a.TimeCreated);
                    break;
                case 3:
                    alerts = alerts.OrderBy(a => a.AlertType.Name).ThenByDescending(a => a.TimeCreated);
                    break;
                case 5:
                    alerts = alerts.OrderBy(a => a.Origin).ThenByDescending(a => a.TimeCreated);
                    break;
                default:
                    alerts = alerts.OrderByDescending(a => a.TimeCreated);
                    break;
            }

            return View(new AlertPagingCreateView(alerts.ToPagedList(No_Of_Page, Size_Of_Page), status, sortOrder, No_Of_Page));
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int id, int PageNo, String sortOrder)
        {
            var viewModel = new AlertDetailsViewModel(AlertRepository.GetById(id), PageNo, sortOrder);
            if (viewModel.Alert == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Edit([Bind(Include = "Id,Severity,TimeCreated,TimeModified,AlertTypeId,RuleId,Notes,Status,Server,Origin,Engine,MessageTypeName,AlertParameters,Assignee")] Alert alert)
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
