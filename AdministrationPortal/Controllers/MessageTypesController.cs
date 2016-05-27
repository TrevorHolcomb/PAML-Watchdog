using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.MessageTypes;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class MessageTypesController : Controller
    {
        //TODO: Add fields dynamically clientside. See: http://formvalidation.io/examples/adding-dynamic-field/
        private static readonly int NUMBER_OF_PARAMETERS = 5;

        [Inject]
        public Repository<MessageType> MessageTypeRepository { get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { get; set; }

        // GET: MessageTypes
        public ActionResult Index()
        {
            return View(MessageTypeRepository.Get());
        }

        // GET: MessageTypes/Details/5
        public ActionResult Details(string name)
        {
            var messageType = MessageTypeRepository.GetByName(name);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // GET: MessageTypes/Create
        public ActionResult Create()
        {
            var parameters = new List<CreateMessageTypeParameterTypeViewModel>();
            for (var i = 0; i < NUMBER_OF_PARAMETERS; i++)
                parameters.Add(new CreateMessageTypeParameterTypeViewModel(null,null,false));
            var viewModel = new CreateMessageTypeViewModel(null, null, parameters);

            viewModel.SupportedParameterTypes = new SelectList(WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypesSupported.Types);
            viewModel.ParameterName = new List<string>();

            return View(viewModel);
        }

        // POST: MessageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMessageTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var messageType = new MessageType
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                };

                List<MessageTypeParameterType> messageTypeParameterType = new List<MessageTypeParameterType>();

                for (int i = 0; i < viewModel.ParameterName.Count; i++)
                    messageTypeParameterType.Add(new MessageTypeParameterType
                    {
                        Name = viewModel.ParameterName[i],
                        Type = viewModel.ParameterType[i]
                    });
                
                messageType.MessageTypeParameterTypes = messageTypeParameterType;
                MessageTypeParameterTypeRepository.InsertRange(messageTypeParameterType);
                MessageTypeRepository.Insert(messageType);
                MessageTypeRepository.Save();
                MessageTypeParameterTypeRepository.Save();
                return RedirectToAction("Index");
                
            }
            viewModel.ParameterName = new List<string>();
            return View(viewModel);
        }
        
        // GET: MessageTypes/Delete/1
        public ActionResult Delete(string id)
        {
            var messageType = MessageTypeRepository.GetByName(id);
            DeleteMessageTypeViewModel viewModel = new DeleteMessageTypeViewModel();
            viewModel.MessageType = messageType;

            if (messageType == null)
            {
                return HttpNotFound();
            }

            bool notSafeToDelete = (messageType.Alerts.Count != 0) || (messageType.Rules.Count != 0) || (messageType.Messages.Count != 0);
            return View(viewModel.canDeleteThisMessageType(!notSafeToDelete));
        }

        // POST: MessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var messageType = MessageTypeRepository.GetByName(id);
            var messageTypeParameterTypes =
                MessageTypeParameterTypeRepository.Get().Where(parameter => parameter.MessageTypeName == messageType.Name);

            MessageTypeParameterTypeRepository.DeleteRange(messageTypeParameterTypes);
            MessageTypeParameterTypeRepository.Save();

            MessageTypeRepository.Delete(messageType);
            MessageTypeRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(int id)
        {
            MessageType messageType = MessageTypeRepository.GetById(id);
            if (messageType == null)
            {
                return HttpNotFound();
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

            if (ModelState.IsValid)
            {
                MessageType messageInDb = MessageTypeRepository.GetByName(messageType.Name);
                if (messageInDb != null)
                {
                    messageInDb = mapNewMessageTypeOntoDbMessageType(messageType);
                }

                MessageTypeRepository.Update(messageInDb);
                MessageTypeRepository.Save();
                return RedirectToAction("Index");
            }

            return View(messageType);
        }

        private MessageType mapNewMessageTypeOntoDbMessageType(MessageType newMessageType)
        {
            MessageType dbMessageType = MessageTypeRepository.GetByName(newMessageType.Name);
            if (dbMessageType != null)
            {
                dbMessageType.Name = newMessageType.Name;
                dbMessageType.Description = newMessageType.Description;
            }
            return dbMessageType;
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
