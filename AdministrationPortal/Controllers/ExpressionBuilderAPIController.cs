using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Ninject;
using Ninject.Infrastructure.Language;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class ExpressionBuilderAPIController : ApiController
    {
        public class MessageTypeParameterTypeExchange
        {
            public MessageTypeParameterTypeExchange(string name, string type)
            {
                this.Name = name;
                this.Type = type;
            }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        [Inject]
        public Repository<MessageType> MessageTypeRepository { private get; set; }
        [Inject]
        public Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { private get; set; }

        // GET: api/MessageTypesAPI/5
        public JsonResult<IEnumerable<MessageTypeParameterTypeExchange>>  Get(string id)
        {
            var collection = MessageTypeRepository.GetByName(id).MessageTypeParameterTypes.Select(e => new MessageTypeParameterTypeExchange(e.Name, e.Type));
            return Json(collection);
        }

        // POST: api/MessageTypesAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MessageTypesAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MessageTypesAPI/5
        public void Delete(int id)
        {
        }
    }
}
