using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Ninject;
﻿using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogWebAPI.Models;

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        [Inject]
        public Repository<UnvalidatedMessage> UnvalidatedMessageRepository { private get; set; }
        [Inject]
        public Repository<MessageType> MessageTypeRepository { private get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { private get; set; }

        #region GET

        // GET: api/Messages/GetMessageTypes
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

        // GET: api/Messages/GetMessageType/id
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
        //POST: api/Messages/PostMessage ---- JSON within body
        [ResponseType(typeof(string))]
        public IHttpActionResult PostMessage(IncomingMessage incomingMessage)
        {
            if (incomingMessage == null)
                return Conflict();

            AddToDatabase(incomingMessage);
            var toReturn = GenerateOutgoingMessage(incomingMessage);
  
            return Ok(toReturn);
        }

        //POST: api/Messages/PostMessageArray ---- JSON within body
        [ResponseType(typeof(string))]
        public IHttpActionResult PostMessageArray(IncomingMessage[] incomingMessages)
        {
            if (incomingMessages == null)
                return Conflict();


            OutGoingMessage[] toReturn = new OutGoingMessage[incomingMessages.Length];

            for(int x = 0; x < incomingMessages.Length; x++)
            {
                AddToDatabase(incomingMessages[x]);
                toReturn[x] = GenerateOutgoingMessage(incomingMessages[x]);
            }

            return Ok(toReturn);
        }

        #endregion


        #region private methods

        private void AddToDatabase(IncomingMessage incomingMessage)
        {
            var toDatabase = new UnvalidatedMessage
            {
                Server = incomingMessage.Server,
                EngineName = incomingMessage.Engine,
                Origin = incomingMessage.Origin,
                MessageTypeName = incomingMessage.MessageTypeName
            };


            foreach (var param in incomingMessage.Params)
            {
                toDatabase.MessageParameters.Add(new UnvalidatedMessageParameter { Value = param.value, Message = toDatabase, Name = param.name });
            }


            UnvalidatedMessageRepository.Insert(toDatabase);
            UnvalidatedMessageRepository.Save();
        }

        private OutGoingMessage GenerateOutgoingMessage(IncomingMessage incomingMessage)
        {
            OutGoingMessage toReturn = new OutGoingMessage
            {
                Server = incomingMessage.Server,
                Engine = incomingMessage.Engine,
                Origin = incomingMessage.Origin,
                MessageTypeName = incomingMessage.MessageTypeName,
                Params = incomingMessage.Params
            };

            return toReturn;
        }

        #endregion

    }
}