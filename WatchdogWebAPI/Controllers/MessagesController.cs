using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogWebAPI.Models;
using Ninject;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        [Inject]
        public Repository<UnvalidatedMessage> UnvalidatedMessageRepository { private get; set; }
        [Inject]
        public Repository<MessageType> messageTypeRepository { private get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> messageTypeParameterTypeRepository { private get; set; }



        #region GET

        // GET: api/Messages
        public IQueryable<MessageTypeDTO> GetMessageTypes()
        {
            var messageTypes = from mt in messageTypeRepository.Get().AsQueryable()
                        select new MessageTypeDTO()
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
            MessageType messageType = messageTypeRepository.GetByName(name);

            var param = from messageParameter in messageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeName == name).AsQueryable()
                        select new APIMessageParameter()
                        {
                            name = messageParameter.Name,
                            value = messageParameter.Type
                        };

            APIMessageParameter[] paramsArray = param.ToArray<APIMessageParameter>();

            if (messageType == null)
            {
                return NotFound();
            }

            MessageTypeDetailDTO toReturn = new MessageTypeDetailDTO
            {
                Name = messageType.Name,
                Description = messageType.Description,
                parameters = paramsArray,
            };

            return Ok(toReturn);  
                  
        }

        #endregion

        #region POST

        //POST: api/Messages ---- JSON within body
        //[ResponseType(typeof(OutGoingMessage))]
        [ResponseType(typeof(string))]
        public IHttpActionResult PostMessage(IncomingMessage incomingMessage)
        {

            if (incomingMessage == null)
                return Conflict();

            UnvalidatedMessage toDatabase = new UnvalidatedMessage
            {
                Server = incomingMessage.Server,
                EngineName = incomingMessage.Engine,
                Origin = incomingMessage.Origin,
                MessageTypeName = incomingMessage.MessageTypeName
            };
                
                
            foreach(APIMessageParameter param in incomingMessage.Params)
            {
                toDatabase.MessageParameters.Add(new UnvalidatedMessageParameter {Value = param.value, Message = toDatabase });
            }


            UnvalidatedMessageRepository.Insert(toDatabase);
            UnvalidatedMessageRepository.Save();


            //incoming message gets moved to new model for return
            OutGoingMessage toReturn = new OutGoingMessage
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

    }
}