using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class EscalationChainLinksController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: EscalationChainLinks
        public ActionResult Index()
        {
            var escalationChainLinks = db.EscalationChainLinks.Include(e => e.NextLink).Include(e => e.NotifyeeGroup);
            return View(escalationChainLinks.ToList());
        }

        // GET: EscalationChainLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = db.EscalationChainLinks.Find(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Create
        public ActionResult Create()
        {
            ViewBag.PreviousEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id");
            ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name");

            return View();
        }

        // POST: EscalationChainLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EscalationChainLinksCreateViewModel chainLinksCreateViewModel)
        {
            var escalationChainLink = new EscalationChainLink
            {
                PreviousLink =
                    db.EscalationChainLinks.SingleOrDefault(
                        e => e.Id == chainLinksCreateViewModel.PreviousEscalationChainLinkId),
                NotifyeeGroup =
                    db.NotifyeeGroups.SingleOrDefault(e => e.Id == chainLinksCreateViewModel.NotifyeeGroupId)
            };

            if (ModelState.IsValid)
            {
                db.EscalationChainLinks.Add(escalationChainLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PreviousEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextLink);
            ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.NotifyeeGroup);

            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = db.EscalationChainLinks.Find(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            //ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextEscalationChainLinkId);
            //ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.NotifyeeGroupId);
            return View(escalationChainLink);
        }

        // POST: EscalationChainLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NextEscalationChainLinkId,NotifyeeGroupId")] EscalationChainLink escalationChainLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escalationChainLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextEscalationChainLinkId);
            //ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.NotifyeeGroupId);
            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = db.EscalationChainLinks.Find(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            return View(escalationChainLink);
        }

        // POST: EscalationChainLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EscalationChainLink escalationChainLink = db.EscalationChainLinks.Find(id);
            db.EscalationChainLinks.Remove(escalationChainLink);
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
