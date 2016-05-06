using System;
using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;
using CommandLine;
using CommandLine.Text;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
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
        public static int Main(string[] args)
        {

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                try
                {
                    GenerateMessages(options);
                    return 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return 2;
        }

        private static void GenerateMessages(Options options)
        {
            var messageRepository = new EFMessageRepository();
            var messageTypeRepository = new EFMessageTypeRepository();

            if (options.Reset)
            {
                messageTypeRepository.Insert(new MessageType
                {
                    Name = "QueueSizeUpdate",
                    Description = "A regular update from the JMS queue containing the number of elements enqueued.",
                    Id = options.QueueSizeMessageTypeId
                });

                messageTypeRepository.Save();
            }

            var factory = new QueueSizeMessageFactory(new[] {"socrates", "plato", "aristotle"},
                new[] {"webapi", "cli"}, options.QueueSizeMessageTypeId);

            for (var i = 0; i < options.QueueSizeMessageCount; i++)
            {
                var message = factory.Build();
                messageRepository.Insert(message);
            }

            messageRepository.Save();

            return;
        }
    }
}