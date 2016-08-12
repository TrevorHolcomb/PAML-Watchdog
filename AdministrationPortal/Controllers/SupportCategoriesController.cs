using System;
using System.ComponentModel;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using AdministrationPortal.ViewModels.SupportCategories;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class SupportCategoriesController : AbstractBaseController
    {
        [Inject]
        public Repository<SupportCategory> SupportCategoryRepository { private get; set; }

        // GET: SupportCategories
        public ActionResult Index(IndexViewModel.ActionType actionPerformed = IndexViewModel.ActionType.None, string supportCategoryName = "", string message = "")
        {
            return View(new IndexSupportCategoryViewModel(actionPerformed, supportCategoryName, message)
            {
                SupportCategories = SupportCategoryRepository.Get()
            });
        }

        // GET: SupportCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupportCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, Description")] SupportCategory supportCategory)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new {actionPerformed = IndexViewModel.ActionType.Warning, message="Could not create Support Category"});

            if (supportCategory.Name == null || supportCategory.Name.Trim() == string.Empty)
                throw new WarningException("Unable to create Support Category: name required.");

            if (supportCategory.Description == null || supportCategory.Name.Trim() == string.Empty)
                throw new WarningException("Unable to create Support Category: description requried.");

            try
            {
                SupportCategoryRepository.GetByName(supportCategory.Name);
            }
            catch (InvalidOperationException)
            {
                SupportCategoryRepository.Insert(supportCategory);
                SupportCategoryRepository.Save();
                return RedirectToAction("Index", new { actionPerformed = IndexViewModel.ActionType.Create, entityName = supportCategory.Name});
            }
            return RedirectToAction("Index", new {message = $"Support Category {supportCategory.Name} already exists", actionPerformed = IndexViewModel.ActionType.Warning});
        }

        // GET: SupportCategories/Edit/5
        public ActionResult Edit(int id)
        {
            var supportCategory = SupportCategoryRepository.GetById(id);
            if (supportCategory == null)
                throw new ArgumentException($"No SupportCategory found with Id: {id}");

            return View(supportCategory);
        }

        // POST: SupportCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Description")] SupportCategory supportCategory)
        {
            if (ModelState.IsValid)
            {
                var supportCategoryInDb = SupportCategoryRepository.GetById(supportCategory.Id);
                if(supportCategoryInDb == null)
                    throw new ArgumentException($"No SupportCategory found with Id: {supportCategory.Id}");

                supportCategoryInDb.Description = supportCategory.Description;
                SupportCategoryRepository.Update(supportCategoryInDb);
                SupportCategoryRepository.Save();
                return RedirectToAction("Index", new { actionPerformed = IndexViewModel.ActionType.Edit, entityName = supportCategoryInDb.Name });
            }

            return View(supportCategory);
        }

        // GET: SupportCategories/Delete/1
        public ActionResult Delete(int id)
        {
            var supportCategory = SupportCategoryRepository.GetById(id);              
            if (supportCategory == null)
                throw new ArgumentException($"No SupportCategory found with Id: {id}");

            bool safeToDelete = (supportCategory.Rules.Count == 0);
            var viewModel = new DeleteSupportCategoryViewModel(supportCategory, safeToDelete);
            return View(viewModel);
        }

        // POST: SupportCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supportCategory = SupportCategoryRepository.GetById(id);
            if (supportCategory == null)
                throw new ArgumentException($"No SupportCategory found with id: {id}");

            bool safeToDelete = (supportCategory.Rules.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteSupportCategoryViewModel(supportCategory, false);
                return View(viewModel);
            }

            SupportCategoryRepository.Delete(supportCategory);
            SupportCategoryRepository.Save();
            return RedirectToAction("Index", new { entityName = supportCategory.Name, actionPerformed = IndexViewModel.ActionType.Delete});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SupportCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
