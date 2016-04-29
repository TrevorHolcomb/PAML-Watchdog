using System.Linq;
using System.Net;
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

        //public class NotifyeesCreateNotifyeeViewModel
        #region Notifyees
        public class NotifyeeViewModel
        {
            public int Id { get; set; }
            public int[] NotifyeeGroupIds { get; set; }
            public string Name { get; set; }
            public string CellPhoneNumber { get; set; }
            public string Email { get; set; }
        }

        // POST: Notifyees/CreateNotifyee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateNotifyee(NotifyeesCreateNotifyeeViewModel vm)
        //{
            // if no notifyee groups have been selected have an empty list to prevent null exception
        // GET: Notifyees/EditNotifyee/4
        public ActionResult EditNotifyee(int id)
        {
            var notifyee = db.Notifyees.Single(e => e.Id == id);
            ViewBag.NotifyeeGroupIds = new MultiSelectList(db.NotifyeeGroups, "Id", "Name");
            return View("CreateNotifyee", notifyee);
        }
        // POST: Notifyees/EditNotifyee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotifyee(NotifyeeViewModel vm)
        {
            // if no notifyee groups have been selected have an empty list to prevent null exception
            if (vm.NotifyeeGroupIds == null)
                vm.NotifyeeGroupIds = new int[] { };

            var notifyee = db.Notifyees.Single(e => e.Id == vm.Id);
            notifyee.Name = vm.Name;
            notifyee.CellPhoneNumber = vm.CellPhoneNumber;
            notifyee.Email = vm.Email;
            notifyee.NotifyeeGroups.Clear();
            db.SaveChanges();

            notifyee.NotifyeeGroups = db.NotifyeeGroups.Where(e => vm.NotifyeeGroupIds.Contains(e.Id)).ToList();
            db.SaveChanges();
            
            ViewBag.NotifyeeGroupIds = new MultiSelectList(db.NotifyeeGroups, "Id", "Name");
            return View("CreateNotifyee", notifyee);
        }
        

        // POST: Notifyees/CreateNotifyee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotifyee(NotifyeeViewModel vm)
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
        public ActionResult DeleteNotifyee(int? id)
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
            return View("DeleteNotifyee",notifyee);
        }
        // POST: Notifyees/Delete/5
        [HttpPost, ActionName("DeleteNotifyee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNotifyeeConfirmed(int id)
        {
            Notifyee notifyee = db.Notifyees.Find(id);
            notifyee.NotifyeeGroups.Clear();
            db.SaveChanges();
            db.Notifyees.Remove(notifyee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region NotifyeeGroup
        public class NotifyeeGroupViewModel
        {
            public int Id { get; set; }
            public int[] NotifyeeIds { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }


        // GET: Notifyees/EditNotifyeeGroup/4
        public ActionResult EditNotifyeeGroup(int id)
        {
            var group = db.NotifyeeGroups.Single(e => e.Id == id);
            ViewBag.NotifyeeIds = new MultiSelectList(db.Notifyees, "Id", "Name");
            return View("CreateNotifyeeGroup", group);
        }
        // POST: Notifyees/EditNotifyeeGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotifyeeGroup(NotifyeeGroupViewModel vm)
        {
            // if no notifyee groups have been selected have an empty list to prevent null exception
            if (vm.NotifyeeIds == null)
                vm.NotifyeeIds = new int[] { };

            var notifyeeGroup = db.NotifyeeGroups.Single(e => e.Id == vm.Id);
            notifyeeGroup.Name = vm.Name;
            notifyeeGroup.Description = vm.Description;
            notifyeeGroup.Notifyees.Clear();
            db.SaveChanges();

            notifyeeGroup.Notifyees = db.Notifyees.Where(e => vm.NotifyeeIds.Contains(e.Id)).ToList();
            db.SaveChanges();

            ViewBag.NotifyeeIds = new MultiSelectList(db.Notifyees, "Id", "Name");
            return View("CreateNotifyeeGroup", notifyeeGroup);
        }

        // POST: Notifyees/CreateNotifyeeGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotifyeeGroup(NotifyeeGroupViewModel vm)
        {
            // If no Notifyees have been selected have an empty list to prevent null exception
            if (vm.NotifyeeIds == null)
                vm.NotifyeeIds = new int[] { };

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


        // GET: Notifyees/DeleteNotifyeeGroup/3
        public ActionResult DeleteNotifyeeGroup(int id)
        {
            return View(db.NotifyeeGroups.Single(e => e.Id == id));
        }
        // POST: Notifyees/DeleteNotifyeeGroup
        [HttpPost, ActionName("DeleteNotifyeeGroup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNotifyeeGroupConfirmed(int id)
        {
            var group = db.NotifyeeGroups.Single(e => e.Id == id);
            group.Notifyees.Clear();
            db.SaveChanges();
            db.NotifyeeGroups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
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
