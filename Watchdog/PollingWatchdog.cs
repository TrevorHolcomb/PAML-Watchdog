using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WatchdogDatabaseAccessLayer;

namespace Watchdog
{
    class PollingWatchdog : AbstractWatchdog
    {
        private static PollingWatchdog sWatchdog;
        private static Timer PollingTimer;
        private const int POLLING_RATE = 5*1000;        //5 seconds for now

        public override AbstractWatchdog GetInstance()
        {
            if (sWatchdog == null)
                sWatchdog = new PollingWatchdog();
            return sWatchdog;
        }

        public override void Watch()
        {
            if (PollingTimer == null)
                PollingTimer = new Timer(ConsumeNewMessages, null, 0, POLLING_RATE);
        }

        protected override void ConsumeNewMessages(Object state)
        {
            WatchdogDatabaseContext dbContext = new WatchdogDatabaseContext();

            List<Message> totalMessages = dbContext.Messages.ToList<Message>();
            Console.WriteLine("watchdog sees " + totalMessages.Count + " total messages");

            //ToList() forces .net to do the query and store in memory, otherwise, or with LINQ, the expression is lazy-evaluated and causes an error in the second loop
            List<Message> messages = dbContext.Messages.Where<Message>(msg => !msg.Processed).ToList<Message>();
            var rules = dbContext.Rules.Select<Rule, Rule>(e=>e).ToList<Rule>();

            foreach (Message message in messages)
            {
                foreach (Rule rule in rules)
                {
                    //TODO: use fancy JSON comparator John mentioned
                    //if (rule.RuleTrigger == /*triggered*/)
                        //create a corresponding alert
                        //foreach (Alert alert in rule.Alerts)
                            //alert.Triggered = true;                   

                    /*TODO: discuss implementation of alert activation. 
                        We should find out what the Notifier expects as input. 
                        But if we were to redesign that too, I'd propose adding a 
                        boolean field, Alerts.Active, analogous to Messages.Processed
                    */
                }
                message.Processed = true;

                //for debugging/simulation
                Console.WriteLine("Consumed: " + message.Id);
            }

            dbContext.Messages.RemoveRange(messages);       //delete messages after processing
            dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }
}
