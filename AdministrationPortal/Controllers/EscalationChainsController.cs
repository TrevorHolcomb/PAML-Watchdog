using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.EscalationChains;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class EscalationChainsController : Controller
    {
        [Inject]
        public IEscalationChainRepository EscalationChainRepository { private get; set; }

        [Inject]
        public IEscalationChainLinkRepository EscalationChainLinkRepository { private get; set; }

        [Inject]
        public INotifyeeGroupRepository NotifyeeGroupRepository { private get; set; }

        // GET: EscalationChains
        public ActionResult Index()
        {
            var escalationChains = EscalationChainRepository.Get();
            return View(escalationChains);
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

            EscalationChainLinkRepository.Insert(rootChainLink);
            EscalationChainRepository.Insert(escalationChain);
            EscalationChainLinkRepository.Save();
            EscalationChainRepository.Save();
            

            return RedirectToAction("Edit", new { id = escalationChain.Id });
        }

        // GET: EscalationChains/EditLink/0
        public ActionResult EditLink(int id)
        {
            ViewBag.NotifyeeGroupId = new SelectList(NotifyeeGroupRepository.Get(), "Id", "Name");
            return View(EscalationChainLinkRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLink(EscalationChainsEditLinkViewModel escalationChainsEditLinkViewModel)
        {
            var chainLink = EscalationChainLinkRepository.GetById(escalationChainsEditLinkViewModel.Id);

            chainLink.NotifyeeGroup =
                NotifyeeGroupRepository.GetById(escalationChainsEditLinkViewModel.NotifyeeGroupId);
            EscalationChainLinkRepository.Update(chainLink);
            EscalationChainLinkRepository.Save();

            EscalationChainLink rootLink;
            for (rootLink = chainLink; rootLink.PreviousLink != null; rootLink = rootLink.PreviousLink){}
            return RedirectToAction("Edit", new {id = rootLink.EscalationChain.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShiftBack(int chainId, int linkId)
        {
            var chain = EscalationChainRepository.GetById(chainId);
            var link = EscalationChainLinkRepository.GetById(linkId);
            var linkIndex = chain.IndexOf(link);

            if (linkIndex == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                chain.RemoveAt(linkIndex);
                chain.InsertAt(link,linkIndex - 1);

                EscalationChainLinkRepository.Save();
                EscalationChainRepository.Save();

                return RedirectToAction("Edit", new { id = chainId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShiftForward(int chainId, int linkId)
        {
            var chain = EscalationChainRepository.GetById(chainId);
            var link = EscalationChainLinkRepository.GetById(linkId);
            var linkIndex = chain.IndexOf(link);

            if (linkIndex == chain.Length() - 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                chain.RemoveAt(linkIndex);
                chain.InsertAt(link, linkIndex + 1);

                EscalationChainLinkRepository.Save();
                EscalationChainRepository.Save();

                return RedirectToAction("Edit", new { id = chainId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveLink(int chainId, int linkId)
        {
            var chain = EscalationChainRepository.GetById(chainId);
            var link = EscalationChainLinkRepository.GetById(linkId);
            var linkIndex = chain.IndexOf(link);

            chain.RemoveAt(linkIndex);

            EscalationChainLinkRepository.Delete(link);
            EscalationChainLinkRepository.Save();

            return RedirectToAction("Edit", new {id = chainId});
        }
        

        // Appends a new EscalationChainLink on to the end of the list.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppendLink(int id)
        {
            var chain = EscalationChainRepository.GetById(id);

            //Traverse to end of chain
            EscalationChainLink link;
            for (link = chain.EscalationChainRootLink; link.NextLink != null; link = link.NextLink) { }
            var newLink = new EscalationChainLink();
            link.NextLink = newLink;

            EscalationChainLinkRepository.Insert(newLink);
            EscalationChainLinkRepository.Update(link);
            EscalationChainLinkRepository.Save();
            
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: EscalationChains/Edit/5
        public ActionResult Edit(int id)
        {
            var escalationChain = EscalationChainRepository.GetById(id);

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
            var model = EscalationChainRepository.GetById(escalationChainEditViewModel.Id);
            model.Name = escalationChainEditViewModel.Name;
            model.Description = escalationChainEditViewModel.Description;

            EscalationChainRepository.Update(model);
            EscalationChainRepository.Save();
            
            return RedirectToAction("Index");
        }

        // GET: EscalationChains/Delete/5
        public ActionResult Delete(int id)
        {
            var escalationChain = EscalationChainRepository.GetById(id);
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
            var escalationChain = EscalationChainRepository.GetById(id);
            var links = new List<EscalationChainLink>();
            var link = escalationChain.EscalationChainRootLink;
            links.Add(link);

            for (; link != null; link = link.NextLink)
            {
                links.Add(link);
            }

            EscalationChainRepository.Delete(escalationChain);

            foreach (var each_link in links)
            {
                EscalationChainLinkRepository.Delete(each_link);
            }

            EscalationChainRepository.Save();
            EscalationChainLinkRepository.Save();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EscalationChainLinkRepository.Dispose();
                EscalationChainRepository.Dispose();
                NotifyeeGroupRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
