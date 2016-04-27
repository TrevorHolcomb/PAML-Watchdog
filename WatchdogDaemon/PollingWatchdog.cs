using System;
using System.Linq;
using System.Threading;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public class PollingWatchdog : AbstractWatchdog
    {

        private Timer _pollingTimer;
        
        private const int PollingRate = 5*1000;        //5 seconds for now

        public PollingWatchdog(WatchdogDatabaseContext dbContext, IRuleEngine ruleEngine) : base(dbContext, ruleEngine)
        {
        }

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
            var totalMessages = DbContext.Messages.ToList<Message>();
            Console.WriteLine("watchdog sees " + totalMessages.Count + " total messages");

            //ToList() forces .net to do the query and store in memory, otherwise, or with LINQ, the expression is lazy-evaluated and causes an error in the second loop
            var messages = DbContext.Messages.Where<Message>(msg => !msg.Processed).ToList<Message>();
            var rules = DbContext.Rules.Select<Rule, Rule>(e=>e).ToList<Rule>();

            RuleEngine.ConsumeMessages(rules, messages);

            // DbContext.Messages.RemoveRange(messages);       //delete messages after processing
            DbContext.SaveChanges();
        }
    }
}
