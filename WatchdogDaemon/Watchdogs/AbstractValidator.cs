using System;
using Ninject;
using Ninject.Syntax;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogDaemon.Watchdogs
{
    public abstract class AbstractValidator : IDisposable
    {

        protected AbstractValidator(IResolutionRoot kernel)
        {
            MessageRepository = kernel.Get<Repository<Message>>();
            MessageTypeRepository = kernel.Get<Repository<MessageType>>();
            UnvalidatedMessageRepository = kernel.Get<Repository<UnvalidatedMessage>>();
            UnvalidatedMessageParameterRepository = kernel.Get<Repository<UnvalidatedMessageParameter>>();
            EngineRepository = kernel.Get<Repository<Engine>>();
        }

        protected readonly Repository<UnvalidatedMessageParameter> UnvalidatedMessageParameterRepository;
        protected readonly Repository<UnvalidatedMessage> UnvalidatedMessageRepository;
        protected readonly Repository<Message> MessageRepository;
        protected readonly Repository<MessageType> MessageTypeRepository;
        protected readonly Repository<Engine> EngineRepository;

        public void Dispose()
        {
            MessageTypeRepository.Dispose();
            MessageRepository.Dispose();
        }

        public abstract bool Validate(UnvalidatedMessage toValidate);

    }
}
