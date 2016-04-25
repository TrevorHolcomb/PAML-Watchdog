using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        #region setup

        public static TheoryData<ICollection<Rule>, ICollection<Message>> TestData => new TheoryData
            <ICollection<Rule>, ICollection<Message>>
        {
            {
                Rules1,
                Messages1
            }
        };

        public static ICollection<Rule> Rules1 = new[]
            {
                new Rule
                {
                    Id = 0,
                    Name = "QueueTooSmall",
                    RuleTrigger = "size < 100"
                }
            };

        public static ICollection<Message> Messages1 = new[]
            {
                new Message
                    {
                        Id = 1,
                        MessageTypeId = 0,
                        Origin = "Athens",
                        Params = "size=1",
                        Processed = false,
                        Server = "Homer"
                    },

                    new Message
                    {
                        Id = 2,
                        MessageTypeId = 0,
                        Origin = "Delphi",
                        Params = "size=3",
                        Processed = false,
                        Server = "TheOracle"
                    },

                    new Message
                    {
                        Id = 3,
                        MessageTypeId = 0,
                        Origin = "Macedonia",
                        Params = "size=5",
                        Processed = false,
                        Server = "Alexander"
                    }
            };
#endregion 

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            var ruleEngine = new RuleEngine();
            ruleEngine.ConsumeMessages(rules, messages);
            Assert.Empty(messages.Where(e => e.Processed == false));
        }
    }
}
