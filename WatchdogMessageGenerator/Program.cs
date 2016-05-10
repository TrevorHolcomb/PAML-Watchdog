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
        [Option('i', "id", Required = true, HelpText = "The QueueMessageTypeId to use")]
        public int QueueSizeMessageTypeId { get; set; }

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
        public static Repository<Message> MessageRepository { get; set; }
        public static Repository<MessageType> MessageTypeRepository { get; set; }
        public static Repository<MessageTypeParameterType> MessageTypeParameterTypeRepository { get; set; }
        public static Repository<MessageParameter> MessageParameterRepository { get; set; }

        public static int Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel(new EFModule()))
            {
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

            var messageType = MessageTypeRepository.GetById(options.QueueSizeMessageTypeId);

            var factory = new QueueSizeMessageFactory(new[] {"dev-machine"},
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
            DeleteMessageType();

            MessageTypeRepository.Save();
            MessageTypeParameterTypeRepository.Save();

            InsertMessageType(options);

            MessageTypeRepository.Save();
            MessageTypeParameterTypeRepository.Save();
        }

        private static void DeleteMessageType()
        {
            var messageTypesToRemove = MessageTypeRepository.Get().Where(messageType => messageType.Name == "QueueSizeUpdate").ToList();
            var messagesToRemove = MessageRepository.Get().Where(message => messageTypesToRemove.Contains(message.MessageType)).ToList();
            var messageParametersToRemove = messagesToRemove.SelectMany(message => message.MessageParameters).ToList();
            var messageTypeParameterTypesToRemove =
                messageParametersToRemove.Select(messageParameter => messageParameter.MessageTypeParameterType).ToList();

            MessageParameterRepository.DeleteRange(messageParametersToRemove);
            MessageTypeParameterTypeRepository.DeleteRange(messageTypeParameterTypesToRemove);
            MessageRepository.DeleteRange(messagesToRemove);
            MessageTypeRepository.DeleteRange(messageTypesToRemove);
        }

        private static void InsertMessageType(Options options)
        {
            var messageType = new MessageType
            {
                Name = "QueueSizeUpdate",
                Description = "A regular update from the JMS queue containing the number of elements enqueued.",
                Id = options.QueueSizeMessageTypeId
            };

            var messageTypeParameterType = new MessageTypeParameterType
            {
                MessageType = messageType,
                Name = "QueueSize",
                Type = "integer"
            };

            messageType.MessageTypeParameterTypes = new List<MessageTypeParameterType> {messageTypeParameterType};
            MessageTypeRepository.Insert(messageType);
            MessageTypeParameterTypeRepository.Insert(messageTypeParameterType);
        }
    }
}