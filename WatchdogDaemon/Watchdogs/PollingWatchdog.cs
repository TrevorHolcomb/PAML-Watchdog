using System.Linq;
using System.Threading;
using Ninject.Syntax;

namespace WatchdogDaemon.Watchdogs
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
            var messages = MessageRepository.Get().Where(msg => !msg.IsProcessed).ToList();
            var rules = RuleRepository.Get().ToList();

            foreach (var message in messages)
            {
                foreach (var rule in rules)
                {
                    var alert = RuleEngine.ConsumeMessage(rule, message);
                    if (alert != null)
                    {
                        AlertRepository.Insert(alert);                        
                    }
                }

                message.IsProcessed = true;
                MessageRepository.Update(message);
            }

            MessageRepository.Save();
            AlertRepository.Save();
        }


        public PollingWatchdog(IResolutionRoot root) : base(root)
        {
        }
    }
}
