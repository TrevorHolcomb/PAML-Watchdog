﻿using System;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public abstract class AbstractWatchdog : IDisposable
    {
        protected AbstractWatchdog(WatchdogDatabaseContext dbContext, IRuleEngine ruleEngine)
        {
            DbContext = dbContext;
            RuleEngine = ruleEngine;
        }

        public WatchdogDatabaseContext DbContext { get; set; }
        protected readonly IRuleEngine RuleEngine;

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public abstract void Watch();
        public abstract void StopWatching();
        protected abstract void Run(object state);
    }
}