using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        #region setup

        public static TheoryData<ICollection<Rule>, ICollection<Message>> TestData => 
            new TheoryData <ICollection<Rule>, ICollection<Message>>
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
                    RuleTrigger = @"{
                        ""server"": ""cisco-1000"",
                        ""orgin"": ""fell from the sky"",
                        ""comparator"": ""lt"",
                        ""variable"": ""queueSize"",
                        ""constant"": 10        
                    }"
                },
                new Rule
                {
                    Id = 1,
                    Name = "QueueTooBig",
                    RuleTrigger = @"{
                        ""comparator"": ""gt"",
                        ""variable"": ""queueSize"",
                        ""constant"": 1000
                    }"
                }
            };

        public static ICollection<Message> Messages1 = new[]
            {
                new Message
                    {
                        Id = 1,
                        MessageTypeId = 0,
                        Params = @"{""size"":""1""}",
                        IsProcessed = false,
                    },

                    new Message
                    {
                        Id = 2,
                        MessageTypeId = 0,
                        Params = @"{""size"":""10""}",
                        IsProcessed = false,
                    },

                    new Message
                    {
                        Id = 3,
                        MessageTypeId = 0,
                        Params = @"{""size"":""1001""}",
                        IsProcessed = false,
                    }
            };

        public static ICollection<Alert> AlertsEmpty = new List<Alert>();

        #endregion

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            //Arrange
            WatchdogDatabaseContainer dbContext = WatchdogDatabaseContextMocker.Mock(rules, messages);
            var ruleEngine = new RuleEngine();
            ruleEngine.dbContext = dbContext;

            //Act
            ruleEngine.ConsumeMessages(rules, messages);

            //Assert
            Assert.Empty(messages.Where(e => e.IsProcessed == false));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestProduceAlerts(ICollection<Rule> rules, ICollection<Message> messages)
        {
            //Arrange
            WatchdogDatabaseContainer dbContext = WatchdogDatabaseContextMocker.Mock(rules, messages);
            var ruleEngine = new RuleEngine();
            ruleEngine.dbContext = dbContext;

            //Act
            ruleEngine.ConsumeMessages(rules, messages);

            //Assert
            int numberOfAlerts = dbContext.Alerts.Count();
            Assert.Equal(1, numberOfAlerts);
        }
    }
}
