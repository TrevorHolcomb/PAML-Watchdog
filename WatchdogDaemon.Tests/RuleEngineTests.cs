using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        public static TheoryData<ICollection<Rule>, ICollection<Message>> TestData => 
            new TheoryData <ICollection<Rule>, ICollection<Message>>
        {
            {
                Rules1,
                Messages1
            },
            {
                Rules2,
                Messages2
            }
        };
        #region TestData1
        public static ICollection<Rule> Rules1 = new[]
        {
            new Rule
            {
                Id = 0,
                Name = "QueueSizeWithinRange",
                RuleTriggerSchema = @"{
                    ""$schema"": ""http://json-schema.org/draft-04/schema"",
                    ""type"": ""object"",
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
        #endregion

        #region TestData2

        public static ICollection<Rule> Rules2 = new List<Rule>
        {
            new Rule
            {
                Name = "QueueSizeTooBig",
                RuleTriggerSchema = @"{
                    ""$schema"": ""http://json-schema.org/draft-04/schema"",
                    ""type"":""object"",
                    ""properties"": {
                        ""params"":{
                            ""type"": ""object"",
                            ""properties"":{
                                ""queueSize"":{
                                    ""type"":""integer"",
                                    ""minimum"":0,
                                    ""maximum"":50
                                }
                            }
                         }
                    }
                }"
            }
        };

        public static ICollection<Message> Messages2 = new List<Message>
        {
            new Message
            {
                Params = @"{
	                ""server"":""Socrates"",
	                ""origin"":""WatchdogWebAPI"",
	                ""messageTypeId"":3,
	                ""params"":{
		                ""queueSize"": 59
	                }
                }"
            }, new Message
            {
                Params = @"{
	                ""server"":""Socrates"",
	                ""origin"":""WatchdogWebAPI"",
	                ""messageTypeId"":3,
	                ""params"":{
		                ""queueSize"": 9
	                }
                }"
            }
        };
        #endregion

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            //Arrange
            var dbContext = WatchdogDatabaseContextMocker.Mock(rules, messages);
            var ruleEngine = new RuleEngine();

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
            var dbContext = WatchdogDatabaseContextMocker.Mock(rules, messages);
            var ruleEngine = new RuleEngine();

            //Act
            var alerts = ruleEngine.ConsumeMessages(rules, messages);

            //Assert
            int numberOfAlerts = alerts.Count;
            Assert.Equal(1, numberOfAlerts);
        }
    }
}