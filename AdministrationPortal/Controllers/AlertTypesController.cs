using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.AlertTypes;

namespace AdministrationPortal.Controllers
{
    public class AlertTypesController : Controller
    {
        [Inject]
        public Repository<AlertType> AlertTypeRepository { private get; set; }

        // GET: AlertTypes
        public ActionResult Index()
        {
            return View(AlertTypeRepository.Get());
        }

        // GET: AlertTypes/Details/5
        public ActionResult Details(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                return HttpNotFound("No AlertType found with Id " + id);
            }
            return View(alertType);
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
                return RedirectToAction("Index");
            }

            return View(alertType);
        }

        // GET: AlertTypes/Edit/5
        public ActionResult Edit(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                return HttpNotFound("No AlertType found with Id " + id);
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
                return HttpNotFound("No AlertType found with Id " + alertType.Id);
            }

            alertTypeInDb.Description = alertType.Description;
            AlertTypeRepository.Update(alertTypeInDb);
            AlertTypeRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: AlertTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                return HttpNotFound();
            }

            bool safeToDelete = (alertType.Alerts.Count == 0 && alertType.Rules.Count == 0);
            var viewModel = new DeleteAlertTypeViewModel(alertType, safeToDelete);

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
                return HttpNotFound("No AlertType found with ID " + id);
            }

            bool safeToDelete = (alertType.Alerts.Count == 0 && alertType.Rules.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteAlertTypeViewModel(alertType, safeToDelete);
                return View(viewModel);
            }

            AlertTypeRepository.Delete(alertType);
            AlertTypeRepository.Save();

            return RedirectToAction("Index");
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
