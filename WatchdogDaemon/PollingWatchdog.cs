using System;
using System.Linq;
using System.Threading;
using Watchdog;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public class PollingWatchdog : AbstractWatchdog
    {

        private Timer _pollingTimer;
        
        private const int PollingRate = 5*1000;        //5 seconds, for now
        

        public override void Watch()
        {
            if (_pollingTimer == null)
                _pollingTimer = new Timer(Run, null, 0, PollingRate);
            else
                _pollingTimer.Change(0, PollingRate);
        }

        public override void StopWatching()
        {
            _pollingTimer?.Change(0, Timeout.Infinite);
        }

        protected override void Run(object state)
        {
            using (var dbContext = ContextProvider.GetDatabaseContext())
            {
                var totalMessages = dbContext.Messages.ToList<Message>();
                Console.WriteLine("watchdog sees " + totalMessages.Count + " total messages");

                //ToList() forces .net to do the query and store in memory, otherwise, or with LINQ, the expression is lazy-evaluated and causes an error in the second loop
                var messages = dbContext.Messages.Where<Message>(msg => !msg.IsProcessed).ToList<Message>();
                var rules = dbContext.Rules.ToList<Rule>();

                var alerts = RuleEngine.ConsumeMessages(rules, messages);
                dbContext.Alerts.AddRange(alerts);
                // DbContext.Messages.RemoveRange(messages);       //delete messages after processing
                dbContext.SaveChanges();
            }
        }

        public PollingWatchdog(IContextProvider provider, IRuleEngine ruleEngine) : base(provider, ruleEngine)
        {
        }
    }
}
