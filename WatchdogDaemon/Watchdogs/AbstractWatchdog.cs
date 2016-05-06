using System;
using Ninject;
using Ninject.Syntax;
using Watchdog.RuleEngine;
using WatchdogDatabaseAccessLayer.Repositories;

namespace Watchdog.Watchdogs
{
    public abstract class AbstractWatchdog : IDisposable
    {
        protected AbstractWatchdog(IResolutionRoot kernel)
        {
            MessageRepository = kernel.Get<IMessageRepository>();
            RuleRepository = kernel.Get<IRuleRepository>();
            AlertRepository = kernel.Get<IAlertRepository>();
            RuleEngine = kernel.Get<IRuleEngine>();
        }

        protected readonly IAlertRepository AlertRepository;
        protected readonly IMessageRepository MessageRepository;
        protected readonly IRuleRepository RuleRepository;
        protected readonly IRuleEngine RuleEngine;

        public void Dispose()
        {
            MessageRepository.Dispose();
        }

        public abstract void Watch();
        public abstract void StopWatching();
        protected abstract void Run(object state);
    }
}