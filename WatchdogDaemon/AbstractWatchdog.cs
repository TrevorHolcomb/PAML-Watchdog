using System;
using System.Data.Entity;
using Watchdog;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public abstract class AbstractWatchdog : IDisposable
    {
        public AbstractWatchdog(IContextProvider provider, IRuleEngine ruleEngine)
        {
            ContextProvider = provider;
            RuleEngine = ruleEngine;
        }

        protected readonly IContextProvider ContextProvider;
        protected readonly IRuleEngine RuleEngine;

        public void Dispose()
        {
            
        }

        public abstract void Watch();
        public abstract void StopWatching();
        protected abstract void Run(object state);
    }
}