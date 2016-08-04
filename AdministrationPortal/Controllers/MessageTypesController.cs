using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.MessageTypes;
using Ninject;
using NLog;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class MessageTypesController : Controller
    {
        [Inject]
        public Repository<MessageType> MessageTypeRepository { get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: MessageTypes
        public ActionResult Index()
        {
            return View(MessageTypeRepository.Get());
        }

        // GET: MessageTypes/Details/5
        public ActionResult Details(string id)
        {
            MessageType messageType;
            try
            {
                messageType = MessageTypeRepository.GetByName(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"No MessageType found with Name: {id}");
            }
            
            return View(messageType);
        }

        // GET: MessageTypes/Create
        public ActionResult Create()
        {
            var viewModel = new CreateMessageTypeViewModel();
            return View(viewModel);
        }

        // POST: MessageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMessageTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var messageType = new MessageType
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
            };

            var messageTypeParameterTypes = GetParameterTypes(viewModel);
            messageType.MessageTypeParameterTypes = messageTypeParameterTypes;
            MessageTypeParameterTypeRepository.InsertRange(messageTypeParameterTypes);
            MessageTypeRepository.Insert(messageType);
            MessageTypeRepository.Save();
            MessageTypeParameterTypeRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: MessageTypes/Delete/1
        public ActionResult Delete(string id)
        {
            MessageType messageType;
            try
            {
                messageType = MessageTypeRepository.GetByName(id);
            }
            catch (InvalidOperationException) {
                throw new ArgumentException($"No MessageType found with Name: {id}");
            }

            bool safeToDelete = (messageType.Alerts.Count == 0 && messageType.Rules.Count == 0 && messageType.Messages.Count == 0);
            var viewModel = new DeleteMessageTypeViewModel(messageType, safeToDelete);
            return View(viewModel);
        }

        // POST: MessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MessageType messageType;
            try
            {
                messageType = MessageTypeRepository.GetByName(id);
            }
            catch (InvalidOperationException)
            { 
                throw new ArgumentException($"No MessageType found with Name: {id}");
            }

            bool safeToDelete = (messageType.Alerts.Count == 0 && messageType.Rules.Count == 0 && messageType.Messages.Count == 0);
            if (!safeToDelete)
            {
                var viewModel = new DeleteMessageTypeViewModel(messageType, false);
                return View(viewModel);
            }

            var messageTypeParameterTypes = MessageTypeParameterTypeRepository.Get()
                .Where(parameter => parameter.MessageTypeName == messageType.Name);

            MessageTypeParameterTypeRepository.DeleteRange(messageTypeParameterTypes);
            MessageTypeParameterTypeRepository.Save();

            MessageTypeRepository.Delete(messageType);
            MessageTypeRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(string id)
        {
            MessageType messageType;
            try
            {
                messageType = MessageTypeRepository.GetByName(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"No MessageType found with Name: {id}");
            }

            return View(messageType);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name, Description")] MessageType messageType)
        {
            if (!ModelState.IsValid)
            {
                return View(messageType);
            }

            MessageType messageTypeInDb;
            try
            {
                messageTypeInDb = MessageTypeRepository.GetByName(messageType.Name);
            }
            catch (InvalidOperationException)
            { 
                throw new ArgumentException($"No MessageType found with Name: {messageType.Name}");
            }

            messageTypeInDb.Description = messageType.Description;
            MessageTypeRepository.Update(messageTypeInDb);
            MessageTypeRepository.Save();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = RedirectToAction("Index");
        }

        private static List<MessageTypeParameterType> GetParameterTypes(CreateMessageTypeViewModel viewModel)
        {
            var messageTypeParameterTypes = new List<MessageTypeParameterType>();

            for (int i = 0; i < viewModel.ParameterNames.Count; i++)
            {
                if (!viewModel.ParametersEnabled[i])
                    continue;

                messageTypeParameterTypes.Add(new MessageTypeParameterType
                {
                    Name = viewModel.ParameterNames[i],
                    Type = viewModel.ParameterTypes[i],
                    Required = viewModel.ParametersRequired[i]
                });
            }

            return messageTypeParameterTypes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MessageTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
