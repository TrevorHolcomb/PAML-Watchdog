using System;
using System.Configuration;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using AdministrationPortal.ViewModels.RegisteredEngines;
using NLog;

namespace AdministrationPortal.Controllers
{
    public class RegisteredEnginesController : Controller
    {
        [Inject]
        public Repository<Engine> EngineRepository { private get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: RegisteredEngines
        public ActionResult Index(IndexViewModel.ActionType actionPerformed = IndexViewModel.ActionType.None, string engineName = "", string message = "")
        {
            return View(new IndexRegisteredEnginesViewModel(actionPerformed, engineName, message)
            {
                Engines = EngineRepository.Get()
            });
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

            //Make sure it doesn't already exist before creating it
            try
            {
                EngineRepository.GetByName(engine.Name);
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Warning,
                    message = $"Engine {engine} already exists"
                });
            }
            catch (InvalidOperationException)
            { 
                EngineRepository.Insert(engine);
                EngineRepository.Save();
            }
            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Create,
                engineName = engine
            });
        }

        // GET: RegisteredEngines/Delete/5
        public ActionResult Delete(string id)
        {
            Engine engine;
            try
            {
                engine = EngineRepository.GetByName(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"No Engine found with name: {id}");
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
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"No Engine found with name: {id}");
            }

            bool safeToDelete = (engineToDelete.Alerts.Count == 0 && engineToDelete.Messages.Count == 0);
            if (!safeToDelete)
            {
                return RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Warning,
                    message = $"Unable to delete engine in use: {id}"
                });
            }

            EngineRepository.Delete(engineToDelete);
            EngineRepository.Save();

            return RedirectToAction("Index", new
            {
                actionPerformed = IndexViewModel.ActionType.Delete,
                engineName = id
            });
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

            if (ConfigurationManager.AppSettings["ExceptionHandlingEnabled"] == bool.TrueString)
            {
                filterContext.ExceptionHandled = true;

                // Redirect on error:
                filterContext.Result = RedirectToAction("Index", new
                {
                    actionPerformed = IndexViewModel.ActionType.Error,
                    id = 0,
                    message = filterContext.Exception.Message
                });
            }
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
