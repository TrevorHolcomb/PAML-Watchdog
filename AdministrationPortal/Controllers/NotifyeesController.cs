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



        // GET: Notifyees/CreateNotifyee
        public ActionResult CreateNotifyeeGroup()
        {
            ViewBag.NotifyeeIds = new MultiSelectList(db.Notifyees, "Id", "Name");
            return View();
        }

        public class NotifyeesCreateNotifyeeGroupViewModel
        {
            public int[] NotifyeeIds { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }


        // POST: Notifyees/CreateNotifyee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotifyeeGroup(NotifyeesCreateNotifyeeGroupViewModel vm)
        {
            // If no Notifyees have been selected have an empty list to prevent null exception
            if (vm.NotifyeeIds == null)
                vm.NotifyeeIds = new int[] {};

            var notifyees = db.Notifyees.Where(e => vm.NotifyeeIds.Contains(e.Id)).ToList();
            var notifyeeGroup = new NotifyeeGroup
            {
                Name = vm.Name,
                Description = vm.Description,
                Notifyees = notifyees,
            };

            db.NotifyeeGroups.Add(notifyeeGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Notifyees/CreateNotifyee
        public ActionResult CreateNotifyee()
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

        // POST: Notifyees/CreateNotifyee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotifyee(NotifyeesCreateNotifyeeViewModel vm)
        {
            // if no notifyee groups have been selected have an empty list to prevent null exception
            if(vm.NotifyeeGroupIds == null)
                vm.NotifyeeGroupIds = new int[] {};

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
