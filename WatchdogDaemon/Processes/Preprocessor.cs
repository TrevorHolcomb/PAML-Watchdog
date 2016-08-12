using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NLog;
using WatchdogDaemon.Exceptions;
using WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogDaemon.Processes
{
    /// <summary>
    /// The Preprocessor takes the raw unvalidated input from the database and maps the input to valid message types. 
    /// </summary>
    public class Preprocessor : IProcess
    {
        [Inject]
        public Repository<UnvalidatedMessageParameter> UnvalidatedMessageParameterRepository { set; get; }
        [Inject]
        public Repository<UnvalidatedMessage> UnvalidatedMessageRepository { set; get; }
        [Inject]
        public Repository<Message> MessageRepository { set; get; }
        [Inject]
        public Repository<MessageType> MessageTypeRepository { set; get; }
        [Inject]
        public Repository<Engine> EngineRepository { set; get; }
        [Inject]
        public Repository<MessageParameter> MessageParameterRepository { set; get; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Dispose()
        {
            MessageRepository.Dispose();
            MessageParameterRepository.Dispose();
            MessageTypeRepository.Dispose();
            UnvalidatedMessageRepository.Dispose();
            UnvalidatedMessageParameterRepository.Dispose();
            EngineRepository.Dispose();
        }

        public void Run()
        {
            var unvalidatedMessages = UnvalidatedMessageRepository.Get();
            foreach (var message in unvalidatedMessages)
            {
                Preprocess(message);
            }
        }

        public void Preprocess(UnvalidatedMessage unvalidated)
        {
            try
            {
                var messageType = PreprocessMessageType(unvalidated);
                var engine = PreprocessEngine(unvalidated);
                var origin = PreprocessOrigin(unvalidated);
                var server = PreprocessServer(unvalidated);
                var parameters = GetValidParameters(unvalidated);

                var message = BuildMessage(messageType, engine, origin, server, parameters);

                MessageRepository.Insert(message);
                MessageParameterRepository.InsertRange(message.MessageParameters);
                MessageRepository.Save();
                MessageParameterRepository.Save();

                Logger.Info($"Message Successfully Preprocessed Of Type {message.MessageType.Name}");
            }
            catch (InvalidParameterException e)
            {
                Logger.Warn(e, $"Invalid Parameters {e}");
            }
            catch(Exception e)
            {
                Logger.Error(e, $"Uncaught Exception {e}");
            }
            finally
            {
                DeleteUnvalidatedEntities(unvalidated);
            }
        }

        private static Message BuildMessage(MessageType messageType, Engine engine, string origin, string server,
            IEnumerable<MessageParameter> parameters)
        {
            var message = new Message
            {
                Engine = engine,
                Origin = origin,
                Server = server,
                MessageType = messageType
            };

            foreach (var parameter in parameters)
            {
                message.MessageParameters.Add(parameter);
                parameter.Message = message;
            }

            return message;
        }

        private Engine PreprocessEngine(UnvalidatedMessage message)
        {
            return EngineRepository.GetByName(message.EngineName);
        }

        private MessageType PreprocessMessageType(UnvalidatedMessage message)
        {
            return MessageTypeRepository.GetByName(message.MessageTypeName);
        }
        
        private static string PreprocessOrigin(UnvalidatedMessage message)
        {
            return message.Origin;
        }

        private static string PreprocessServer(UnvalidatedMessage message)
        {
            return message.Server;
        }

        private IEnumerable<MessageParameter> GetValidParameters(UnvalidatedMessage unvalidatedMessage)
        {
            var messageTypeToValidateAgainst = MessageTypeRepository.GetByName(unvalidatedMessage.MessageTypeName);
            var parameterTypes =  messageTypeToValidateAgainst.MessageTypeParameterTypes;

            var validatedParameters = new List<MessageParameter>();
            foreach (var parameterType in parameterTypes)
            {
                var parameterInstance =
                    unvalidatedMessage.MessageParameters.Single(parameter => parameter.Name == parameterType.Name);
                if (IsValidParameter(parameterInstance, parameterType))
                {
                    validatedParameters.Add(parameterInstance.ToMessageParameter(parameterType));
                }
                else
                {
                    throw new InvalidParameterException(
                        $"Parameter '{parameterType.Name}' whose type is '{parameterType.Type}', has an invalid value of '{parameterInstance.Value}' ");
                }
            }

            return validatedParameters;
        }

        private static bool IsValidParameter(UnvalidatedMessageParameter parameterInstance, MessageTypeParameterType parameterType)
        {
            return TypeHandlerList.TypeHandlers[parameterType.Name].IsValid(parameterInstance.Value);
        }

        private void DeleteUnvalidatedEntities(UnvalidatedMessage toValidate)
        {
            UnvalidatedMessageParameterRepository.DeleteRange(toValidate.MessageParameters.ToList());
            UnvalidatedMessageRepository.Delete(toValidate);

            UnvalidatedMessageParameterRepository.Save();
            UnvalidatedMessageRepository.Save();
        }
    }
}

