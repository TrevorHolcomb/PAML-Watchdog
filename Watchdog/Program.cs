using System;
using System.Linq;
using System.Data.Entity;
using WatchdogDatabaseAccessLayer;
using WatchdogMessageGenerator;
using System.Threading;

namespace Watchdog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Watchdog simulator started");

            //start producer
            const int PRODUCER_FREQUENCY = 3 * 1000;        //3 seconds
            Database.SetInitializer(new EntityBase());
            Timer producer = new Timer(Produce, null, 0, PRODUCER_FREQUENCY);

            //start consumer
            AbstractWatchdog rex = new PollingWatchdog().GetInstance();
            rex.Watch();

            while (true) { }
        }

        private static void Produce(Object state)
        {
            WatchdogDatabaseContext db = new WatchdogDatabaseContext();

            var factory = new QueueSizeMessageFactory(new[] { "socrates", "plato", "aristotle" },
                    new[] { "webapi", "cli" });

            for (var i = 0; i < 3; i++)
            {
                var message = new Message { };

                //prevent duplicate primary key
                bool duplicate = true;
                while (duplicate)
                { 
                    message = factory.Build();
                    var target = from msg in db.Messages
                                 where msg.Id == message.Id
                                 select msg;
                    duplicate = target.ToList<Message>().Count != 0;
                }

                db.Messages.Add(message);
                Console.WriteLine("Produced: " + message.Id);
            }
        
            db.SaveChanges();
        }
    }
}
