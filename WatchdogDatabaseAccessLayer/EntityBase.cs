using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer
{
    public class EntityBase : DropCreateDatabaseAlways<WatchdogDatabaseContext>
    {
        protected override void Seed(WatchdogDatabaseContext context)
        {
            context.MessageTypes.AddOrUpdate(new MessageType { Description = "a regular update from a JMS queue containing the size of the queue.", Name = "QueueSizeUpdate", Id = 0, RequiredParams = "size" });
            context.SaveChanges();
        }
    }
}
