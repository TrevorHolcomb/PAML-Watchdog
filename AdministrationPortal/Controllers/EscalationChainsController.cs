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
using WatchdogDatabaseAccessLayer.ModelHelpers;

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

        // GET: EscalationChains/Create
        public ActionResult Create()
        {
            var rootChainLink = new EscalationChainLink();
            var escalationChain = new EscalationChain
            {
                Name = "NewEscalationChain",
                Description = "A brand new escalation chain that hasn't been configured yet",
                EscalationChainRootLink = rootChainLink
            };

            db.EscalationChainLinks.Add(rootChainLink);
            db.EscalationChains.Add(escalationChain);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = escalationChain.Id });
        }

        // GET: EscalationChains/EditLink/0
        public ActionResult EditLink(int? id)
        {
            ViewBag.NotifyeeGroupId = new SelectList(db.NotifyeeGroups, "Id", "Name");
            return View(db.EscalationChainLinks.Single(e => e.Id == id));
        }

        public class EscalationChainsEditLinkViewModel
        {
            public int NotifyeeGroupId { get; set; }
            public int Id { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLink(EscalationChainsEditLinkViewModel escalationChainsEditLinkViewModel)
        {
            var chainLink = db.EscalationChainLinks.Single(e => e.Id == escalationChainsEditLinkViewModel.Id);

            chainLink.NotifyeeGroup =
                db.NotifyeeGroups.Single(e => e.Id == escalationChainsEditLinkViewModel.NotifyeeGroupId);
            db.Entry(chainLink).State = EntityState.Modified;
            db.SaveChanges();


            EscalationChainLink rootLink;
            for (rootLink = chainLink; rootLink.PreviousLink != null; rootLink = rootLink.PreviousLink){}
            return RedirectToAction("Edit", new {id = rootLink.EscalationChain.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShiftBack(int chainId, int linkId)
        {
            var chain = db.EscalationChains.Single(e => e.Id == chainId);
            var link = db.EscalationChainLinks.Single(e => e.Id == linkId);
            var linkIndex = chain.IndexOf(link);

            if (linkIndex == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                chain.RemoveAt(linkIndex);
                chain.InsertAt(link,linkIndex - 1);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = chainId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShiftForward(int chainId, int linkId)
        {
            var chain = db.EscalationChains.Single(e => e.Id == chainId);
            var link = db.EscalationChainLinks.Single(e => e.Id == linkId);
            var linkIndex = chain.IndexOf(link);
            if (linkIndex == chain.Length() - 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                chain.RemoveAt(linkIndex);
                chain.InsertAt(link, linkIndex + 1);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = chainId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveLink(int chainId, int linkId)
        {
            var chain = db.EscalationChains.Single(e => e.Id == chainId);
            var link = db.EscalationChainLinks.Single(e => e.Id == linkId);
            var linkIndex = chain.IndexOf(link);

            chain.RemoveAt(linkIndex);
            db.EscalationChainLinks.Remove(link);
            db.SaveChanges();

            return RedirectToAction("Edit", new {id = chainId});
        }
        

        // Appends a new EscalationChainLink on to the end of the list.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppendLink(int? id)
        {
            var chain = db.EscalationChains.Single(e => e.Id == id);

            //Traverse to end of chain
            EscalationChainLink link;
            for (link = chain.EscalationChainRootLink; link.NextLink != null; link = link.NextLink) { }
            var newLink = new EscalationChainLink();
            link.NextLink = newLink;
            db.EscalationChainLinks.Add(newLink);
            db.Entry(link).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: EscalationChains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationChain escalationChain = db.EscalationChains.Find(id);
            if (escalationChain == null)
            {
                return HttpNotFound();
            }

            return View(escalationChain);
        }

        // POST: EscalationChains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public class EscalationChainEditViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EscalationChainEditViewModel escalationChainEditViewModel)
        {
            var model = db.EscalationChains.Single(e => e.Id == escalationChainEditViewModel.Id);
            model.Name = escalationChainEditViewModel.Name;
            model.Description = escalationChainEditViewModel.Description;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
        public ActionResult DeleteConfirmed(int id)
        {
            EscalationChain escalationChain = db.EscalationChains.Find(id);
            
            List<EscalationChainLink> links = new List<EscalationChainLink>();
            EscalationChainLink link = escalationChain.EscalationChainRootLink;
            links.Add(link);
            for (; link != null; link = link.NextLink)
            {
                links.Add(link);
            }

            db.EscalationChains.Remove(escalationChain);
            db.EscalationChainLinks.RemoveRange(links);
            
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
