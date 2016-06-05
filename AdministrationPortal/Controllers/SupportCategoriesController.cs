using System.Web.Mvc;
using AdministrationPortal.ViewModels.SupportCategories;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class SupportCategoriesController : Controller
    {
        [Inject]
        public Repository<SupportCategory> SupportCategoryRepository { private get; set; }

        // GET: SupportCategories
        public ActionResult Index()
        {
            return View(SupportCategoryRepository.Get());
        }

        // GET: SupportCategories/Details/5
        public ActionResult Details(int id)
        {
            var supportCategory = SupportCategoryRepository.GetById(id);
            if (supportCategory == null)
            {
                return HttpNotFound();
            }
            return View(supportCategory);
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
            {
                return HttpNotFound();
            }
            return View(supportCategory);
        }

        // POST: SupportCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Description")] SupportCategory supportCategory)
        {
            if (ModelState.IsValid)
            {
                SupportCategory supportCategoryInDb = SupportCategoryRepository.GetById(supportCategory.Id);
                if(supportCategoryInDb == null)
                {
                    return HttpNotFound();
                }

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
            {
                return HttpNotFound();
            }

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
            {
                return HttpNotFound();
            }

            bool safeToDelete = (supportCategory.Rules.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteSupportCategoryViewModel(supportCategory, safeToDelete);
                return View(viewModel);
            }

            SupportCategoryRepository.Delete(supportCategory);
            SupportCategoryRepository.Save();
            return RedirectToAction("Index");
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
