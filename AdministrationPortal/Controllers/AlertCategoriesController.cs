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
            return View(alertCategory);
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
        public ActionResult Create([Bind(Include = "Id,CategoryName")] AlertCategory alertCategory)
        {
            if (ModelState.IsValid)
            {
                AlertCategoryRepository.Insert(alertCategory);
                AlertCategoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View(alertCategory);
        }

        // GET: AlertCategories/Edit/5
        public ActionResult Edit(int id)
        {
            
            var alertCategory = AlertCategoryRepository.GetById(id);
            if (alertCategory == null)
                throw new ArgumentException($"No AlertCategory found with Id: {id}");
            return View(alertCategory);
        }

        // POST: AlertCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName")] AlertCategory alertCategory)
        {
            if (ModelState.IsValid)
            {
                AlertCategoryRepository.Update(alertCategory);
                AlertCategoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View(alertCategory);
        }

        // GET: AlertCategories/Delete/5
        public ActionResult Delete(int id)
        {
            
            var alertCategory = AlertCategoryRepository.GetById(id);
            if (alertCategory == null)
                throw new ArgumentException($"No AlertCategory found with Id: {id}");
            return View(alertCategory);
        }

        // POST: AlertCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var alertCategory = AlertCategoryRepository.GetById(id);
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
