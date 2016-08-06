﻿using System;
using System.Configuration;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.AlertTypes;
using NLog;

namespace AdministrationPortal.Controllers
{
    public class AlertTypesController : Controller
    {
        [Inject]
        public Repository<AlertType> AlertTypeRepository { private get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: AlertTypes
        public ActionResult Index(IndexViewModel.ActionType actionPerformed = IndexViewModel.ActionType.None, string alertTypeName = "", string message = "")
        {
            return View(new IndexAlertTypesViewModel(actionPerformed, alertTypeName, message)
            {
                AlertTypes = AlertTypeRepository.Get()
            });
        }

        // GET: AlertTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlertTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] AlertType alertType)
        {
            if (ModelState.IsValid)
            {
                AlertTypeRepository.Insert(alertType);
                AlertTypeRepository.Save();
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Create,
                    alertTypeName = alertType.Name
                });
            }

            return View(alertType);
        }

        // GET: AlertTypes/Edit/5
        public ActionResult Edit(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                throw new ArgumentException($"No AlertType found with Id {id}");
            }

            return View(alertType);
        }

        // POST: AlertTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] AlertType alertType)
        {
            if (!ModelState.IsValid)
            {
                return View(alertType);
            }
            
            AlertType alertTypeInDb = AlertTypeRepository.GetById(alertType.Id);
            if (alertTypeInDb == null)
            {
                throw new ArgumentException($"No AlertType found with Id {alertType.Id}");
            }

            alertTypeInDb.Description = alertType.Description;
            AlertTypeRepository.Update(alertTypeInDb);
            AlertTypeRepository.Save();

            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Edit,
                alertTypeName = alertTypeInDb.Name
            });
        }

        // GET: AlertTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                throw new ArgumentException($"No AlertType found with Id {id}");
            }

            bool safeToDelete = (alertType.Alerts.Count == 0 && alertType.Rules.Count == 0);
            var viewModel = new DeleteAlertTypesViewModel(alertType, safeToDelete);

            return View(viewModel);
        }

        // POST: AlertTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null )
            {
                throw new ArgumentException($"No AlertType found with Id {id}");
            }

            bool safeToDelete = (alertType.Alerts.Count == 0 && alertType.Rules.Count == 0);
            if (!safeToDelete)
            {
                //var viewModel = new DeleteAlertTypesViewModel(alertType, false);
                //return View(viewModel);
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Warning,
                    message = $"Unable to delete AlertType in use: {alertType}"
                });
            }

            AlertTypeRepository.Delete(alertType);
            AlertTypeRepository.Save();

            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Delete,
                alertTypeName = alertType.Name
            });
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

            if (ConfigurationManager.AppSettings["ExceptionHandlingEnabled"] == bool.TrueString)
            {
                filterContext.ExceptionHandled = true;

                // Redirect on error:
                filterContext.Result = RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Error,
                    id = 0,
                    message = filterContext.Exception.Message
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AlertTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
