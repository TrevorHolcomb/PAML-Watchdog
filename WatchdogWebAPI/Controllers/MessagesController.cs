using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WatchdogDatabaseAccessLayer;
using NJsonSchema;
using Newtonsoft.Json;
using WatchdogWebAPI.Models;

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();
        private const string ORIGIN = "WatchdogWebAPI";

        #region GET

        // GET: api/Messages
        public IQueryable<MessageTypeDTO> GetMessageTypes()
        {
            var messageTypes = from mt in db.MessageTypes
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
            MessageType messageType = db.MessageTypes.Find(id);

            if (messageType == null)
            {
                return NotFound();
            }

            MessageTypeDetailDTO toReturn = new MessageTypeDetailDTO
            {
                Id = messageType.Id,
                Name = messageType.Name,
                Descrpiton = messageType.Description,
                Schema = messageType.MessageSchema
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

            //The new model JSON is validated against the requested message type schema
            bool isValidMessage = isValid(JsonConvert.SerializeObject(incomingMessage),incomingMessage.MessageTypeId);
            //if properly formatted message create a new message object to add to database
            if (isValidMessage)
            {
                
                Message toDatabase = new Message
                {
                    Server = incomingMessage.Server,
                    Origin = ORIGIN,
                    MessageTypeId = incomingMessage.MessageTypeId,
                    Params = JsonConvert.SerializeObject(incomingMessage.Params),
                    IsProcessed = false
                };

                db.Messages.Add(toDatabase);
                db.SaveChanges();

                //incoming message gets moved to new model for return
                OutGoingMessage toReturn = new OutGoingMessage
                {
                    Server = incomingMessage.Server,
                    Origin = ORIGIN,
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

        private bool isValid(string messageJson,int messageTypeId)
        {
            string schema = db.MessageTypes.Find(messageTypeId).MessageSchema;

            if (schema == null)
                return false;

            JsonSchema4 incomingMessageSchema = JsonSchema4.FromJson(schema);
            var result = incomingMessageSchema.Validate(messageJson);

            if (result.Count == 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}