using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.Controllers
{
    public class EscalationChainLinksController : Controller
    {
        private WatchdogDatabaseContext db = new WatchdogDatabaseContext();

        // GET: EscalationChainLinks
        public async Task<ActionResult> Index()
        {
            var escalationChainLinks = db.EscalationChainLinks.Include(e => e.NextEscalationChainLink).Include(e => e.NotifyeeGroup);
            return View(await escalationChainLinks.ToListAsync());
        }

        // GET: EscalationChainLinks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = await db.EscalationChainLinks.FindAsync(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Create
        public ActionResult Create()
        {
            ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id");
            ViewBag.Id = new SelectList(db.NotifyeeGroups, "Id", "Name");
            return View();
        }

        // POST: EscalationChainLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NextEscalationChainLinkId,NotifyeeGroupId")] EscalationChainLink escalationChainLink)
        {
            if (ModelState.IsValid)
            {
                db.EscalationChainLinks.Add(escalationChainLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextEscalationChainLinkId);
            ViewBag.Id = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.Id);
            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = await db.EscalationChainLinks.FindAsync(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextEscalationChainLinkId);
            ViewBag.Id = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.Id);
            return View(escalationChainLink);
        }

        // POST: EscalationChainLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NextEscalationChainLinkId,NotifyeeGroupId")] EscalationChainLink escalationChainLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escalationChainLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NextEscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChainLink.NextEscalationChainLinkId);
            ViewBag.Id = new SelectList(db.NotifyeeGroups, "Id", "Name", escalationChainLink.Id);
            return View(escalationChainLink);
        }

        // GET: EscalationChainLinks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChainLink escalationChainLink = await db.EscalationChainLinks.FindAsync(id);
            if (escalationChainLink == null)
            {
                return HttpNotFound();
            }
            return View(escalationChainLink);
        }

        // POST: EscalationChainLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EscalationChainLink escalationChainLink = await db.EscalationChainLinks.FindAsync(id);
            db.EscalationChainLinks.Remove(escalationChainLink);
            await db.SaveChangesAsync();
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
