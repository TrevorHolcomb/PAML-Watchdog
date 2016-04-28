using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using WatchdogDatabaseAccessLayer;
using WebGrease.Css.Extensions;

namespace AdministrationPortal.Controllers
{
    public class NotifyeesController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: Notifyees
        public ActionResult Index()
        {
            return View(new NotifyeesIndexViewModel
            {
                NotifyeeGroups = db.NotifyeeGroups.ToList(),
                Notifyees = db.Notifyees.ToList()
            });
        }


        // GET: Notifyees/Create
        public ActionResult Create()
        {
            ViewBag.NotifyeeGroupIds = new MultiSelectList(db.NotifyeeGroups, "Id", "Name");
            return View();
        }

        public class NotifyeesCreateNotifyeeViewModel
        {
            public int[] NotifyeeGroupIds { get; set; }
            public string Name { get; set; }
            public string CellPhoneNumber { get; set; }
            public string Email { get; set; }
        }

        // POST: Notifyees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotifyeesCreateNotifyeeViewModel vm)
        {
            var notifyeeGroups = db.NotifyeeGroups.Where(e => vm.NotifyeeGroupIds.Contains(e.Id)).ToList();
            var notifyee = new Notifyee
            {
                Name = vm.Name,
                Email = vm.Email,
                CellPhoneNumber = vm.CellPhoneNumber,
                NotifyeeGroups = notifyeeGroups,
            };

            db.Notifyees.Add(notifyee);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        
        // GET: Notifyees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifyee notifyee = db.Notifyees.Find(id);
            
            if (notifyee == null)
            {
                return HttpNotFound();
            }
            return View(notifyee);
        }

        // POST: Notifyees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notifyee notifyee = db.Notifyees.Find(id);
            notifyee.NotifyeeGroups.Clear();
            db.SaveChanges();
            db.Notifyees.Remove(notifyee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
