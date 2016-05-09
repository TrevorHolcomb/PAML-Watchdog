using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;
using CommandLine;
using CommandLine.Text;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;

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
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    public class Program
    {
        public static IRepository<Message> MessageRepository { get; set; }
        public static IRepository<MessageType> MessageTypeRepository { get; set; }
        public static IRepository<MessageTypeParameterType> MessageTypeParameterTypeRepository { get; set; }

        public static int Main(string[] args)
        {
            MessageRepository = new EFMessageRepository();
            MessageTypeRepository = new EFMessageTypeRepository();
            MessageTypeParameterTypeRepository = new EFMessageTypeParameterTypeRepository();

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options)) return -1;
            GenerateMessages(options);
            return 0;
        }

        private static void GenerateMessages(Options options)
        {
        
            if (options.Reset)
            {
                Reset(options);
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
                Type = "integer",
            };

            messageType.MessageTypeParameterTypes = new List<MessageTypeParameterType>() {messageTypeParameterType};
            MessageTypeRepository.Insert(messageType);
            MessageTypeParameterTypeRepository.Insert(messageTypeParameterType);

            MessageTypeRepository.Save();
            MessageTypeParameterTypeRepository.Save();
        }
    }
}