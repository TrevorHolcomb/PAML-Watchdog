using System;
using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.RuleCategories;
using NLog;

namespace AdministrationPortal.Controllers
{
    public class RuleCategoriesController : Controller
    {
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { private get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: RuleCategories
        public ActionResult Index()
        {
            return View(RuleCategoryRepository.Get());
        }

        // GET: RuleCategories/Details/5
        public ActionResult Details(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            if (ruleCategory == null)
                throw new ArgumentException("No Rule found with id " + id);

            return View(ruleCategory);
        }

        
        // GET: RuleCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RuleCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, Description")] RuleCategory ruleCategory)
        {
            if (ModelState.IsValid)
            {
                RuleCategoryRepository.Insert(ruleCategory);
                RuleCategoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View(ruleCategory);
        }

        // GET: RuleCategories/Edit/5
        public ActionResult Edit(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            if (ruleCategory == null)
                throw new ArgumentException($"No RuleCategory found with Id: {id}");

            return View(ruleCategory);
        }

        // POST: RuleCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Description")] RuleCategory ruleCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ruleCategory);
            }

            var ruleCategoryInDb = RuleCategoryRepository.GetById(ruleCategory.Id);
            if (ruleCategoryInDb == null)
                throw new ArgumentException($"No RuleCategory found with Id: {ruleCategory.Id}");

            ruleCategoryInDb.Description = ruleCategory.Description;
            RuleCategoryRepository.Update(ruleCategoryInDb);
            RuleCategoryRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: RuleCategories/Delete/5
        public ActionResult Delete(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            if (ruleCategory == null)
                throw new ArgumentException($"No RuleCategory found with Id: {id}");

            bool safeToDelete = (ruleCategory.Rules.Count == 0);
            var viewModel = new DeleteRuleCategoryViewModel(ruleCategory, safeToDelete);
            return View(viewModel);
        }

        // POST: RuleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            if (ruleCategory == null)
                throw new ArgumentException($"No RuleCategory found with Id: {id}");

            bool safeToDelete = (ruleCategory.Rules.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteRuleCategoryViewModel(ruleCategory, false);
                return View(viewModel);
            }

            RuleCategoryRepository.Delete(ruleCategory);
            RuleCategoryRepository.Save();

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
                RuleCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
