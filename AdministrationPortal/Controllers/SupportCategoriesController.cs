using System;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.SupportCategories;
using Ninject;
using NLog;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class SupportCategoriesController : Controller
    {
        [Inject]
        public Repository<SupportCategory> SupportCategoryRepository { private get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: SupportCategories
        public ActionResult Index()
        {
            return View(SupportCategoryRepository.Get());
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
            if (ModelState.IsValid)
            {
                SupportCategoryRepository.Insert(supportCategory);
                SupportCategoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View(supportCategory);
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
                SupportCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
