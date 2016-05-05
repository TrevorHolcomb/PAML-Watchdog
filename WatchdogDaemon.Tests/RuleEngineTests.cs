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
                Name = "QueueSizeWithinRange",
                RuleTriggerSchema = @"{
                    ""$schema"": ""http://json-schema.org/draft-04/schema"",
                    ""type"": ""object"",
                    ""title"": ""Queue Size Within Range"",
                    ""description"": ""checks that messages from any server and origin report a queue size within range"",
                    ""properties"": {
                        ""params"": {
                            ""type"": ""object"",
                            ""properties"": {
                                ""server"": {},
                                ""origin"": {},
                                ""queueSize"": {
                                    ""type"": ""integer"",
                                    ""minimum"": 0,
                                    ""maximum"": 100
                                }
                            }
                        }
                    }
                }"
            },
                                            
            new Rule
            {
                Id = 1,
                Name = "QueueTooBig",
                RuleTriggerSchema = @"{
                    ""$schema"": ""http://json-schema.org/draft-04/schema"",
                    ""type"":""object"",
                    ""title"": ""Queue Too Big"",
                    ""description"": ""checks that queue sizes from testServer2 aren't too high"",
                    ""properties"": {
                        ""server"": {""enum"": [""testServer2""]},
                        ""origin"": {},
                        ""params"": {
                            ""type"": ""object"",
                            ""properties"": {
                                ""queueSize"": {
                                    ""type"": ""integer"",
                                    ""minimum"": 0,
                                    ""maximum"": 50,
                                    ""exclusiveMaximum"": true
                                }
                            }
                        }
                    }
                }"
            }
        };

        public static ICollection<Message> Messages1 = new[]
        {
            new Message
            {
                Id = 2,
                MessageTypeId = 0,
                Params = @"{
                    ""server"": ""testServer1"",
                    ""origin"": ""WatchdogWebAPI"",
                    ""messageTypeId"": 0,
                    ""params"": {
                        ""queueSize"": 10
                    }
                }",
                IsProcessed = false,
            },
            new Message
            {
                Id = 2,
                MessageTypeId = 0,
                Params = @"{
                    ""server"": ""testServer2"",
                    ""origin"": ""FileUpload"",
                    ""messageTypeId"": 0,
                    ""params"": {
                        ""queueSize"": 50
                    }
                }",
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
            foreach (Message message in dbContext.Messages)
                message.IsProcessed = false;

            //Act
            ruleEngine.ConsumeMessages(rules, messages);

            //Assert
            int numberOfAlertsGenerated = dbContext.Alerts.Count();
            Assert.Equal(1, numberOfAlertsGenerated);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestIgnoreConsumedMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            //Arrange
            WatchdogDatabaseContainer dbContext = WatchdogDatabaseContextMocker.Mock(rules, messages);
            var ruleEngine = new RuleEngine();
            ruleEngine.dbContext = dbContext;
            foreach (Message message in dbContext.Messages)
                message.IsProcessed = true;

            //Act
            ruleEngine.ConsumeMessages(rules, messages);

            //Assert
            int numberOfAlertsGenerated = dbContext.Alerts.Count();
            Assert.Equal(0, numberOfAlertsGenerated);
        }
    }
}