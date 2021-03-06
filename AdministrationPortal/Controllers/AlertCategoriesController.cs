﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.AlertCategories;
using Ninject;
using NLog;

namespace AdministrationPortal
{
    public class AlertCategoriesController : Controller
    {
        [Inject]
        public Repository<AlertCategory> AlertCategoryRepository { private get; set; }
        [Inject]
        public Repository<AlertCategoryItem> AlertCategoryItemRepository { private get; set; }
        [Inject]
        public Repository<AlertType> AlertTypeRepository  { private get; set; }
        [Inject]
        public Repository<Engine> EngineRepository { private get; set; }


        // GET: AlertCategories
        public ActionResult Index()
        {
            return View(AlertCategoryRepository.Get().ToList());
        }

        // GET: AlertCategories/Details/5
        public ActionResult Details(int id)
        {
            var alertCategory = AlertCategoryRepository.GetById(id);
            if (alertCategory == null)
                throw new ArgumentNullException(nameof(alertCategory));
            var alertCategoryItem = AlertCategoryItemRepository.Get().Where(a => a.AlertCategoryId == alertCategory.Id).First();
            List<AlertType> alertCategoryAlertTypes = new List<AlertType>();
            var alertCategoryItems = AlertCategoryItemRepository.Get().Where(a => a.AlertCategoryId == alertCategory.Id).ToList();
            foreach(var categoryItem in alertCategoryItems)
            {
                var alertType = AlertTypeRepository.GetById(categoryItem.AlertTypeId);
                alertCategoryAlertTypes.Add(alertType);
            }
            var viewModel = new AlertCategoryDetailsViewModel()
            {
                AlertCategory = alertCategory,
                AlertTypes = alertCategoryAlertTypes,
                Server = alertCategoryItem.Server,
                Engine = alertCategoryItem.Engine,
                Origin = alertCategoryItem.Origin
            };
            
            return View(viewModel);
        }

        // GET: AlertCategories/Create
        public ActionResult Create()
        {
            var viewModel = new AlertCategoryCreateViewModel();
            viewModel.AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            viewModel.EngineList = new SelectList(EngineRepository.Get(), "Name", "Name");
            return View(viewModel);
        }

        // POST: AlertCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlertCategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new AlertCategory()
                {
                    CategoryName = viewModel.CategoryName
                };

                AlertCategoryRepository.Insert(newCategory);
                AlertCategoryRepository.Save();
                newCategory = AlertCategoryRepository.Get().Last();
                foreach(var id in viewModel.SelectedAlertTypes)
                {
                    var newCategoryItem = new AlertCategoryItem()
                    {
                        Server = viewModel.Server,
                        Engine = viewModel.Engine,
                        Origin = viewModel.Origin,
                        AlertTypeId = id,
                        AlertCategoryId = newCategory.Id

                    };
                    AlertCategoryItemRepository.Insert(newCategoryItem);
                    AlertCategoryItemRepository.Save();
                }
                return RedirectToAction("Index");
            }
            viewModel.AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            viewModel.EngineList = new SelectList(EngineRepository.Get(), "Name", "Name");
            return View(viewModel);
        }

        // GET: AlertCategories/Edit/5
        public ActionResult Edit(int id)
        {
            var alertCategory = AlertCategoryRepository.GetById(id);
            if (alertCategory == null)
                throw new ArgumentNullException(nameof(alertCategory));
            var alertCategoryItem = AlertCategoryItemRepository.Get().Where(a => a.AlertCategoryId == alertCategory.Id).First();
            var viewModel = new AlertCategoryEditViewModel()
            {
                AlertCategory = alertCategory,
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EngineList = new SelectList(EngineRepository.Get(), "Name", "Name"),
                Server = alertCategoryItem.Server,
                Origin = alertCategoryItem.Origin,
                Engine = alertCategoryItem.Engine
            };
            
            return View(viewModel);
        }

        // POST: AlertCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlertCategoryEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var alertCategory = AlertCategoryRepository.GetById(viewModel.AlertCategory.Id);
                alertCategory.CategoryName = viewModel.AlertCategory.CategoryName;
                var alertCategoryItems = AlertCategoryItemRepository.Get().Where(cat => cat.AlertCategoryId == viewModel.AlertCategory.Id);
                foreach(var categoryItem in alertCategoryItems)
                {
                    AlertCategoryItemRepository.Delete(categoryItem);
                    AlertCategoryItemRepository.Save();
                }
                foreach (var id in viewModel.SelectedAlertTypes)
                {
                    var newCategoryItem = new AlertCategoryItem()
                    {
                        Server = viewModel.Server,
                        Engine = viewModel.Engine,
                        Origin = viewModel.Origin,
                        AlertTypeId = id,
                        AlertCategoryId = alertCategory.Id

                    };
                    AlertCategoryItemRepository.Insert(newCategoryItem);
                    AlertCategoryItemRepository.Save();
                }
                AlertCategoryRepository.Update(alertCategory);
                AlertCategoryRepository.Save();

                return RedirectToAction("Index");
            }
            viewModel.AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name");
            viewModel.EngineList = new SelectList(EngineRepository.Get(), "Name", "Name");
            return View(viewModel);
        }

        // GET: AlertCategories/Delete/5
        public ActionResult Delete(int id)
        { 
            var alertCategory = AlertCategoryRepository.GetById(id);
            if (alertCategory == null)
                throw new ArgumentNullException(nameof(alertCategory));
            var alertCategoryItem = AlertCategoryItemRepository.Get().Where(a => a.AlertCategoryId == alertCategory.Id).First();
            List<AlertType> alertCategoryAlertTypes = new List<AlertType>();
            var alertCategoryItems = AlertCategoryItemRepository.Get().Where(a => a.AlertCategoryId == alertCategory.Id).ToList();
            foreach (var categoryItem in alertCategoryItems)
            {
                var alertType = AlertTypeRepository.GetById(categoryItem.AlertTypeId);
                alertCategoryAlertTypes.Add(alertType);
            }
            var viewModel = new AlertCategoryDetailsViewModel()
            {
                AlertCategory = alertCategory,
                AlertTypes = alertCategoryAlertTypes,
                Server = alertCategoryItem.Server,
                Engine = alertCategoryItem.Engine,
                Origin = alertCategoryItem.Origin
            };
            return View(viewModel);
        }

        // POST: AlertCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var alertCategory = AlertCategoryRepository.GetById(id);
            var alertCategoryItems = AlertCategoryItemRepository.Get().Where(cat => cat.AlertCategoryId == alertCategory.Id);
            foreach(var categoryItem in alertCategoryItems)
            {
                AlertCategoryItemRepository.Delete(categoryItem);
                AlertCategoryItemRepository.Save();
            }
            AlertCategoryRepository.Delete(alertCategory);
            AlertCategoryRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AlertCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
