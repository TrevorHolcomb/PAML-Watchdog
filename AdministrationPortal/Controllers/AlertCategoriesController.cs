﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.AlertCategories;
using Ninject;

namespace AdministrationPortal.Controllers
{
    public class AlertCategoriesController : AbstractBaseController
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
        public ActionResult Index(IndexViewModel.ActionType actionPerformed = IndexViewModel.ActionType.None, string alertCategoryName = "", string message = "")
        {
            return View(new IndexAlertCategoriesViewModel(actionPerformed, alertCategoryName, message)
            {
                AlertCategories = AlertCategoryRepository.Get()
            });
        }

        // GET: AlertCategories/Details/5
        public ActionResult Details(int id)
        {
            var alertCategory = AlertCategoryRepository.GetById(id);

            if (alertCategory == null)
                throw new ArgumentNullException(nameof(alertCategory));

            var alertCategoryItem = AlertCategoryItemRepository.Get().First(a => a.AlertCategoryId == alertCategory.Id);
            var alertCategoryAlertTypes = new List<AlertType>();
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
            var viewModel = new AlertCategoryCreateViewModel()
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EngineList = new SelectList(EngineRepository.Get(), "Name", "Name")
            };

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
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Create,
                    alertCategoryName = newCategory.CategoryName
                });
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

            var alertCategoryItem = AlertCategoryItemRepository.Get().First(a => a.AlertCategoryId == alertCategory.Id);
            var selectedAlertTypeIds = AlertCategoryItemRepository.Get()
                    .Where(ac => ac.AlertCategoryId == alertCategory.Id)
                    .Select(ac => ac.AlertTypeId).ToList();
            var viewModel = new AlertCategoryEditViewModel()
            {
                AlertTypes = new SelectList(AlertTypeRepository.Get(), "Id", "Name"),
                EngineList = new SelectList(EngineRepository.Get(), "Name", "Name"),
                CategoryName = alertCategory.CategoryName,
                Server = alertCategoryItem.Server,
                Origin = alertCategoryItem.Origin,
                Engine = alertCategoryItem.Engine,
                SelectedAlertTypes = selectedAlertTypeIds
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
                var alertCategory = AlertCategoryRepository.GetById(viewModel.Id);
                alertCategory.CategoryName = viewModel.CategoryName;
                var alertCategoryItems = AlertCategoryItemRepository.Get().Where(cat => cat.AlertCategoryId == viewModel.Id);
                foreach (var categoryItem in alertCategoryItems)
                {
                    AlertCategoryItemRepository.Delete(categoryItem);
                }
                AlertCategoryItemRepository.Save();

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

                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Edit,
                    alertCategoryName = alertCategory.CategoryName
                });
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

            var alertCategoryItem = AlertCategoryItemRepository.Get().First(a => a.AlertCategoryId == alertCategory.Id);
            var alertCategoryAlertTypes = new List<AlertType>();
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
            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Delete,
                alertCategoryName = alertCategory.CategoryName
            });
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
