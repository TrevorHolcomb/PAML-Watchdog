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
    public class MessageTypesController : Controller
    {
        private readonly WatchdogDatabaseContainer _db = new WatchdogDatabaseContainer();

        // GET: MessageTypes
        public async Task<ActionResult> Index()
        {
            return View(await _db.MessageTypes.ToListAsync());
        }

        // GET: MessageTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await _db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // GET: MessageTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,RequiredParams,OptionalParams")] MessageType messageType)
        {
            if (ModelState.IsValid)
            {
                _db.MessageTypes.Add(messageType);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(messageType);
        }

        // GET: MessageTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await _db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // POST: MessageTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,RequiredParams,OptionalParams")] MessageType messageType)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(messageType).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(messageType);
        }

        // GET: MessageTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await _db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // POST: MessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MessageType messageType = await _db.MessageTypes.FindAsync(id);
            _db.MessageTypes.Remove(messageType);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
