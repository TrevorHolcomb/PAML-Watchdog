using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Ninject;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogWebAPI.Models;

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        [Inject]
        public Repository<Message> MessageRepository { private get; set; }
        [Inject]
        public Repository<MessageParameter> MessageParameterRepository { private get; set; }
        [Inject]
        public Repository<MessageType> MessageTypeRepository { private get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { private get; set; }



        #region GET

        // GET: api/Messages
        public IQueryable<MessageTypeDTO> GetMessageTypes()
        {
            var messageTypes = from mt in MessageTypeRepository.Get().AsQueryable()
                        select new MessageTypeDTO
                        {
                            Name = mt.Name,
                            Descrpiton = mt.Description
                        };

            return messageTypes;
        }

        // GET: api/Messages/id
        [ResponseType(typeof(MessageTypeDetailDTO))]
        public IHttpActionResult GetMessageType(string name)
        {
            var messageType = MessageTypeRepository.GetByName(name);

            var param = from messageParameter in MessageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeName == name).AsQueryable()
                        select new APIMessageParameter
                        {
                            name = messageParameter.Name,
                            value = messageParameter.Type
                        };

            var paramsArray = param.ToArray();

            if (messageType == null)
            {
                return NotFound();
            }

            var toReturn = new MessageTypeDetailDTO
            {
                Name = messageType.Name,
                Description = messageType.Description,
                parameters = paramsArray
            };

            return Ok(toReturn);  
                  
        }

        #endregion

        #region POST
        //TODO: This needs to be cleaned up and broken into multiple methods.
        //POST: api/Messages ---- JSON within body
        //[ResponseType(typeof(OutGoingMessage))]
        [ResponseType(typeof(string))]
        public IHttpActionResult PostMessage(IncomingMessage incomingMessage)
        {
            
            var isValidMessage = IsValid(incomingMessage);
            
            //if properly formatted message create a new message object to add to database
            if (!isValidMessage) return Conflict();
            var toDatabase = new Message
            {
                Server = incomingMessage.Server,
                //Engine = incomingMessage.Engine,
                Origin = incomingMessage.Origin,
                MessageTypeName = incomingMessage.MessageTypeName,
                IsProcessed = false
            };

            //insert Message
            MessageRepository.Insert(toDatabase);
            MessageRepository.Save();

                           
            foreach (var param in incomingMessage.Params){

                //validation should catch a fail before this point
                var toInsert = new MessageParameter
                {
                    MessageId = toDatabase.Id,
                    Value = param.value
                };
                //insert parameters
                MessageParameterRepository.Insert(toInsert);
                MessageParameterRepository.Save();
            }


            //incoming message gets moved to new model for return
            var toReturn = new OutGoingMessage
            {
                Server = incomingMessage.Server,
                Engine = incomingMessage.Engine,
                Origin = incomingMessage.Origin,
                MessageTypeName = incomingMessage.MessageTypeName,
                Params = incomingMessage.Params
            };

            return Ok(toReturn);
        }

        #endregion

        #region private methods

        private static bool IsValid(IncomingMessage messageToValidate)
        {
            if (messageToValidate == null)
                return false;

            var valid = WatchdogValidator.validateServer(messageToValidate.Server);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateEngine(messageToValidate.Engine);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateOrigin(messageToValidate.Origin);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateParameters(messageToValidate.Params, messageToValidate.MessageTypeName);
            if (!valid)
                return false;


            return true;
        }

        #endregion
    }
}