using System;
using System.Data.Entity;
using WatchdogDatabaseAccessLayer;

namespace WatchdogMessageGenerator
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Database.SetInitializer(new EntityBase());
            using (var db = new WatchdogDatabaseContext())
            {
                var factory = new QueueSizeMessageFactory(new[] {"socrates", "plato", "aristotle"},
                    new[] {"webapi", "cli"});

                for (var i = 0; i < 10; i++)
                {
                    var message = factory.Build();
                    db.Messages.Add(message);
                }

                db.SaveChanges();

                return 0;
            }
        }
    }
}