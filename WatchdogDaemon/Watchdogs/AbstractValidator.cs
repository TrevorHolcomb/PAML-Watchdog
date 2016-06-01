using System;
using Ninject;
using Ninject.Syntax;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.Watchdogs
{
    public abstract class AbstractValidator : IDisposable
    {

        protected AbstractValidator(IResolutionRoot kernel)
        {
            MessageRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<Message>>();
            MessageTypeRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<MessageType>>();
            UnvalidatedMessageRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<UnvalidatedMessage>>();
        }


        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<UnvalidatedMessage> UnvalidatedMessageRepository;
        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<Message> MessageRepository;
        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<MessageType> MessageTypeRepository;
         

        public void Dispose()
        {
            MessageTypeRepository.Dispose();
            MessageRepository.Dispose();
        }

        public abstract bool Validate(Message toValidate);

    }
}
