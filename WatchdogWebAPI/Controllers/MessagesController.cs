using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogWebAPI.Models;
using Ninject;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        [Inject]
        public Repository<Message> messageRepository { private get; set; }
        [Inject]
        public Repository<MessageParameter> messageParameterRepository { private get; set; }
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
                            Id = mt.Id,
                            Name = mt.Name,
                            Descrpiton = mt.Description
                        };

            return messageTypes;
        }

        // GET: api/Messages/id
        [ResponseType(typeof(MessageTypeDetailDTO))]
        public IHttpActionResult GetMessageType(int id)
        {
            MessageType messageType = messageTypeRepository.GetById(id);

            var param = from messageParameter in messageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeId == id).AsQueryable()
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
                Id = messageType.Id,
                Name = messageType.Name,
                Descrpiton = messageType.Description,
                parameters = paramsArray
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
            
            bool isValidMessage = isValid(incomingMessage);
            
            //if properly formatted message create a new message object to add to database
            if (isValidMessage)
            {
                
                Message toDatabase = new Message
                {
                    Server = incomingMessage.Server,
                    //Engine = incomingMessage.Engine,
                    Origin = incomingMessage.Origin,
                    MessageTypeId = incomingMessage.MessageTypeId,
                    IsProcessed = false
                };

                //insert Message
                messageRepository.Insert(toDatabase);
                messageRepository.Save();


                MessageType messageType = messageTypeRepository.GetById(incomingMessage.MessageTypeId);

                //gets the parameter types of inserted message
                var parameterTypes = messageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeId == incomingMessage.MessageTypeId).AsQueryable();
                            

                foreach (APIMessageParameter param in incomingMessage.Params){

                    //find the parameter type id
                    //validation should catch a fail before this point
                    int paramId = parameterTypes.First(curParam => curParam.Name.Equals(param.name)).Id;


                    MessageParameter toInsert = new MessageParameter
                    {
                        MessageId = toDatabase.Id,
                        Value = param.value,
                    };
                    //insert parameters
                    messageParameterRepository.Insert(toInsert);
                    messageParameterRepository.Save();
                }


                //incoming message gets moved to new model for return
                OutGoingMessage toReturn = new OutGoingMessage
                {
                    Server = incomingMessage.Server,
                    Engine = incomingMessage.Engine,
                    Origin = incomingMessage.Origin,
                    MessageTypeId = incomingMessage.MessageTypeId,
                    Params = incomingMessage.Params
                };

                return Ok(toReturn);
            }
            //Message was not properly formatted
            else
            {
                return Conflict();
            }
        }

        #endregion

        #region private methods

        private bool isValid(IncomingMessage messageToValidate)
        {
            bool valid = true;

            if (messageToValidate == null)
                return false;

            valid = WatchdogValidator.validateServer(messageToValidate.Server);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateEngine(messageToValidate.Engine);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateOrigin(messageToValidate.Origin);
            if (!valid)
                return false;

            valid = WatchdogValidator.validateParameters(messageToValidate.Params, messageToValidate.MessageTypeId);
            if (!valid)
                return false;


            return valid;
        }

        #endregion
    }
}