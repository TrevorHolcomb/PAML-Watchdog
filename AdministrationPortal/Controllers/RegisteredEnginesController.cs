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
            if (ModelState.IsValid)
            {
                if (EngineRepository.GetByName(engine.Name) == null)
                {
                    EngineRepository.Insert(engine);
                    EngineRepository.Save();
                }
                return RedirectToAction("Index");
            }
            return View(engine);
        }

        // GET: RegisteredEngines/Edit/5
        public ActionResult Edit(string name)
        {
            Engine engine = EngineRepository.GetByName(name);

            if (engine == null)
            {
                return HttpNotFound();
            }

            return View(engine);
        }

        // POST: RegisteredEngines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name")] Engine engine)
        {
            if (ModelState.IsValid)
            {
                Engine toUpdate = EngineRepository.GetByName(engine.Name);

                if (toUpdate == null)
                {
                    return HttpNotFound();
                }

                
                EngineRepository.Update()
            }
            
            return View("Create", engine);
        }*/

        // GET: RegisteredEngines/Delete/5
        public ActionResult Delete(string name)
        {
            Engine engine = EngineRepository.GetByName(name);
            if (engine == null)
            {
                return View(engine);
                //return HttpNotFound();
            }
            return View(engine);
        }

        // POST: RegisteredEngines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Engine toDelete = EngineRepository.GetByName(id);
            if (toDelete == null)
            {
                return HttpNotFound();
            }

            //TODO: inform user that the engine is in use
            if (toDelete.Alerts.Count != 0 || toDelete.Messages.Count != 0 || toDelete.Alerts.Count != 0)
                return RedirectToAction("Index");

            EngineRepository.Delete(toDelete);
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
