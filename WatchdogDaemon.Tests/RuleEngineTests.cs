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
                    RuleTrigger = @"{ ""lt"" : { ""size"" : ""1""}}"
                },
                new Rule
                {
                    Id = 1,
                    Name = "QueueTooBig",
                    RuleTrigger = @"{ ""gt"" : { ""size"" : ""3""}}"
                }
            };

        public static ICollection<Message> Messages1 = new[]
            {
                new Message
                    {
                        Id = 1,
                        MessageTypeId = 0,
                        //Origin = "Athens",
                        Params = @"{""size"":""1""}",
                        IsProcessed = false,
                        //Server = "Homer"
                    },

                    new Message
                    {
                        Id = 2,
                        MessageTypeId = 0,
                        //Origin = "Delphi",
                        Params = @"{""size"":""3""}",
                        IsProcessed = false,
                        //Server = "TheOracle"
                    },

                    new Message
                    {
                        Id = 3,
                        MessageTypeId = 0,
                        //Origin = "Macedonia",
                        Params = @"{""size"":""5""}",
                        IsProcessed = false,
                        //Server = "Alexander"
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
