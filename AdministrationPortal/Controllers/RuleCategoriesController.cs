using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class RuleCategoriesController : Controller
    {
        [Inject]
        public Repository<RuleCategory> RuleCategoryRepository { private get; set; }

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
            {
                return HttpNotFound();
            }
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
            {
                return HttpNotFound();
            }
            return View(ruleCategory);
        }

        // POST: RuleCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Description")] RuleCategory ruleCategory)
        {
            if (ModelState.IsValid)
            {
                RuleCategoryRepository.Update(ruleCategory);
                RuleCategoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View(ruleCategory);
        }

        // GET: RuleCategories/Delete/5
        public ActionResult Delete(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            if (ruleCategory == null)
            {
                return HttpNotFound();
            }
            return View(ruleCategory);
        }

        // POST: RuleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ruleCategory = RuleCategoryRepository.GetById(id);
            RuleCategoryRepository.Delete(ruleCategory);
            RuleCategoryRepository.Save();

            return RedirectToAction("Index");
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
