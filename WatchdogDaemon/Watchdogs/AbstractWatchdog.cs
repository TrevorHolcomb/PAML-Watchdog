using System;
using Ninject;
using Ninject.Syntax;
using WatchdogDaemon.RuleEngine;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogDaemon.Watchdogs
{
    public abstract class AbstractWatchdog : IDisposable
    {
        protected AbstractWatchdog(IResolutionRoot kernel)
        {
            MessageRepository = kernel.Get<Repository<Message>>();
            RuleRepository = kernel.Get<Repository<Rule>>();
            AlertRepository = kernel.Get<Repository<Alert>>();
            RuleEngine = kernel.Get<IRuleEngine>();
            Validator = kernel.Get<AbstractValidator>();
            UnvalidatedMessageRepository = kernel.Get<Repository<UnvalidatedMessage>>();
        }

        protected readonly AbstractValidator Validator;

        protected readonly Repository<Alert> AlertRepository;
        protected readonly Repository<Message> MessageRepository;
        protected readonly Repository<Rule> RuleRepository;
        protected readonly Repository<UnvalidatedMessage> UnvalidatedMessageRepository;
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