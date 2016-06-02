using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

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
                return HttpNotFound();
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
                return HttpNotFound();
            }
            return View(alertType);
        }

        // POST: AlertTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] AlertType alertType)
        {
            if (ModelState.IsValid)
            {
                AlertTypeRepository.Update(alertType);
                AlertTypeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(alertType);
        }

        // GET: AlertTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
            if (alertType == null)
            {
                return HttpNotFound();
            }
            return View(alertType);
        }

        // POST: AlertTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var alertType = AlertTypeRepository.GetById(id);
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
