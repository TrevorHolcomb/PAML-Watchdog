using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using Ninject;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogMessageGenerator
{
    internal class Options
    {
        [Option('i', "id", Required = true, HelpText = "The QueueMessageTypeName to use")]
        public string QueueSizeMessageTypeName { get; set; }

        [Option('c', "count", Required = true, HelpText = "The number of messages to create")]
        public int QueueSizeMessageCount { get; set; }

        [Option('r',"reset", Required = false, DefaultValue = false, HelpText = "ReInserts the QueueSizeUpdateMessage MessageType")]
        public bool Reset { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    public class Program
    {
        public static Repository<Engine> EngineRepository { get; set; }
        public static Repository<Message> MessageRepository { get; set; }
        public static Repository<MessageType> MessageTypeRepository { get; set; }
        public static Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { get; set; }
        public static Repository<MessageParameter> MessageParameterRepository { get; set; }

        private const string engineName = "WatchdogMessageGenerator";

        public static int Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel(new EFModule()))
            {
                EngineRepository = kernel.Get<Repository<Engine>>();
                MessageRepository = kernel.Get<Repository<Message>>();
                MessageTypeRepository = kernel.Get<Repository<MessageType>>();
                MessageTypeParameterTypeRepository = kernel.Get<Repository<MessageTypeParameterType>>();
                MessageParameterRepository = kernel.Get<Repository<MessageParameter>>();

                var options = new Options();
                if (!Parser.Default.ParseArguments(args, options)) return -1;
                GenerateMessages(options);
            }
            return 0;
        }

        private static void GenerateMessages(Options options)
        {
        
            if (options.Reset)
            {
                Reset(options);
                return;
            }

            var messageType = MessageTypeRepository.GetByName(options.QueueSizeMessageTypeName);

            Engine engine = EngineRepository.GetByName(engineName);

            var factory = new QueueSizeMessageFactory(engine, new[] {"dev-machine"},
                new[] {"message-generator"}, messageType);

            for (var i = 0; i < options.QueueSizeMessageCount; i++)
            {
                var message = factory.Build();
                MessageRepository.Insert(message);
            }

            MessageRepository.Save();
        }

        private static void Reset(Options options)
        {
            DeleteMessageType(options);
            MessageTypeRepository.Save();
            DeleteEngine();

            InsertEngine();
            InsertMessageType(options);
            MessageTypeRepository.Save();
        }

        private static void DeleteEngine()
        {
            Engine engineToRemove = EngineRepository.GetByName(engineName);
            if (engineToRemove != null)
                EngineRepository.Delete(engineToRemove);
            EngineRepository.Save();
        }

        private static void InsertEngine()
        {
            EngineRepository.Insert(new Engine() { Name = engineName } );
            EngineRepository.Save();
        }

        private static void DeleteMessageType(Options options)
        {
            var messageTypeToRemove = MessageTypeRepository.GetByName(options.QueueSizeMessageTypeName);
            if (messageTypeToRemove == null)
                return;
          
            var messagesToRemove = messageTypeToRemove.Messages.ToList();
            var messageParametersToRemove = messagesToRemove.SelectMany(message => message.MessageParameters).ToList();
            var messageTypeParameterTypesToRemove = messageTypeToRemove.MessageTypeParameterTypes.ToList();

            MessageRepository.DeleteRange(messagesToRemove);
            MessageParameterRepository.DeleteRange(messageParametersToRemove);
            MessageTypeParameterTypeRepository.DeleteRange(messageTypeParameterTypesToRemove);
            MessageTypeRepository.Delete(messageTypeToRemove);
        }

        private static void InsertMessageType(Options options)
        {
            var messageType = new MessageType
            {
                Name = options.QueueSizeMessageTypeName,
                Description = "A regular update from the JMS queue containing the number of elements enqueued.",
            };

            var messageTypeParameterType = new MessageTypeParameterType
            {
                MessageType = messageType,
                Name = "QueueSize",
                Type = "integer"
            };

            messageType.MessageTypeParameterTypes = new List<MessageTypeParameterType> {messageTypeParameterType};

            if (MessageTypeRepository.GetByName(options.QueueSizeMessageTypeName) == null)
            {
                MessageTypeRepository.Insert(messageType);
                MessageTypeParameterTypeRepository.Insert(messageTypeParameterType);
            } else
            {
                System.Console.WriteLine("Skipping insertion of MessageType " + messageType.Name + " since it already exists.");
            }
        }
    }
}