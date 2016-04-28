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

namespace AdministrationPortal.Controllers
{
    public class EscalationChainsController : Controller
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: EscalationChains
        public async Task<ActionResult> Index()
        {
            var escalationChains = db.EscalationChains;
            return View(await escalationChains.ToListAsync());
        }

        // GET: EscalationChains/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChain escalationChain = await db.EscalationChains.FindAsync(id);
            if (escalationChain == null)
            {
                return HttpNotFound();
            }
            return View(escalationChain);
        }

        // GET: EscalationChains/Create
        public ActionResult Create()
        {
            ViewBag.EscalationChainRootLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id");
            return View();
        }

        // POST: EscalationChains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EscalationChainCreateViewModel escalationChainViewModel)
        {
            var escalationChain = new EscalationChain
            {
                EscalationChainRootLink = db.EscalationChainLinks.Single(e => e.Id == escalationChainViewModel.EscalationChainRootLinkId),
                Name = escalationChainViewModel.Name,
                Description = escalationChainViewModel.Description
            };

            if (ModelState.IsValid)
            {
                db.EscalationChains.Add(escalationChain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EscalationChainRootLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id");
            return View(escalationChain);
        }

        // GET: EscalationChains/Edit/5
        /*public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChain escalationChain = await db.EscalationChains.FindAsync(id);
            if (escalationChain == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChain.EscalationChainLinkId);
            return View(escalationChain);
        }*/

        // POST: EscalationChains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,EscalationChainLinkId")] EscalationChain escalationChain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escalationChain).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.EscalationChainLinkId = new SelectList(db.EscalationChainLinks, "Id", "Id", escalationChain.EscalationChainLinkId);
            return View(escalationChain);
        }*/

        // GET: EscalationChains/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChain escalationChain = await db.EscalationChains.FindAsync(id);
            if (escalationChain == null)
            {
                return HttpNotFound();
            }
            return View(escalationChain);
        }

        // POST: EscalationChains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EscalationChain escalationChain = await db.EscalationChains.FindAsync(id);
            db.EscalationChains.Remove(escalationChain);
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
