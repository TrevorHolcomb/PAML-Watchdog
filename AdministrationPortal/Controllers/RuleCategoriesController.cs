using System;
using System.ComponentModel;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.RuleCategories;

namespace AdministrationPortal.Controllers
{
    public class RuleCategoriesController : AbstractBaseController
    {
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { private get; set; }

        // GET: RuleCategories
        public ActionResult Index(IndexViewModel.ActionType actionPerformed = IndexViewModel.ActionType.None, string ruleCategoryName = "", string message = "")
        {
            return View(new IndexRuleCategoriesViewModel(actionPerformed, ruleCategoryName, message)
            {
                RuleCategories = RuleCategoryRepository.Get()
            });
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
            if (ruleCategory?.Name == null)
                throw new WarningException("Unable to create Rule Category: name is required.");

            if (ruleCategory.Description == null || ruleCategory.Name.Trim() == string.Empty)
                throw new WarningException("Unable to create Rule Category: description requried.");

            if (ModelState.IsValid)
            {
                RuleCategoryRepository.Insert(ruleCategory);
                RuleCategoryRepository.Save();
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Create,
                    ruleCategoryName = ruleCategory.Name
                });
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

            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Edit,
                ruleCategoryName = ruleCategoryInDb.Name
            });
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

            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Delete,
                ruleCategoryName = ruleCategory.Name
            });
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
