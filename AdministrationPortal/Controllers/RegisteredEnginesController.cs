using System.Web.Mvc;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.RegisteredEngines;

namespace AdministrationPortal.Controllers
{
    public class RegisteredEnginesController : Controller
    {
        [Inject]
        public Repository<Engine> EngineRepository { private get; set; }

        // GET: RegisteredEngines
        public ActionResult Index()
        {
            return View(EngineRepository.Get());
        }

        // GET: RegisteredEngines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisteredEngines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Engine engine)
        {
            if (!ModelState.IsValid)
            {
                return View(engine);
            }

            try
            {
                EngineRepository.GetByName(engine.Name);
            }
            catch (System.InvalidOperationException)
            { 
                EngineRepository.Insert(engine);
                EngineRepository.Save();
            }
            return RedirectToAction("Index");

        }

        // GET: RegisteredEngines/Delete/5
        public ActionResult Delete(string id)
        {
            Engine engine;
            try
            {
                engine = EngineRepository.GetByName(id);
            }
            catch (System.InvalidOperationException)
            {
                return HttpNotFound("No Engine found with Name: " + id);
            }

            bool safeToDelete = (engine.Alerts.Count == 0 && engine.Messages.Count == 0);
            var viewModel = new DeleteRegisteredEngineViewModel(engine, safeToDelete);
            return View(viewModel);
        }

        // POST: RegisteredEngines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Engine engineToDelete;
            try
            {
                engineToDelete = EngineRepository.GetByName(id);
            }
            catch (System.InvalidOperationException)
            {
                return HttpNotFound("No Engine found with Name: " + id);
            }

            bool safeToDelete = (engineToDelete.Alerts.Count == 0 && engineToDelete.Messages.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteRegisteredEngineViewModel(engineToDelete, safeToDelete);
            }

            EngineRepository.Delete(engineToDelete);
            EngineRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EngineRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
