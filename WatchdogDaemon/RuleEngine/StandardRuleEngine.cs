using System;
using Watchdog.RuleEngine;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine
{
    public class StandardRuleEngine : IRuleEngine
    {
        public Alert ConsumeMessage(Rule rule, Message message)
        {
            throw new NotImplementedException();
        }

        private static Alert CreateAlert(Rule rule, Message message)
        {
            throw new NotImplementedException();
        }
    }
}