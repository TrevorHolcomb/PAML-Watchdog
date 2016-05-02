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

namespace WatchdogWebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        private WatchdogDatabaseContainer db = new WatchdogDatabaseContainer();

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            return Ok();
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            return Ok();
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            return Ok();
        }
    }
}