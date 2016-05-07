using System.Collections.Generic;
using System.Linq;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        public static TheoryData<Rule, Message> ShouldGenerateAlertData = new TheoryData<Rule, Message>
        {
            {
                new Rule
                {
                    Expression = "queueSize > 1000"
                }, new Message
                {
                    MessageColumns = new List<MessageColumn>
                    {
                        new MessageColumn
                        {
                            Value = "10000", MessageTypeColumn = new MessageTypeColumn
                            {
                                Name = "queueSize",
                                Type = "integer"
                            }
                        }
                    }
                }
            }
        };

        public static TheoryData<Rule, Message> ShouldntGenerateAlertData = new TheoryData<Rule, Message>
        {
            {
                new Rule
                {
                    Expression = "queueSize > 1000"
                }, new Message
                {
                    MessageColumns = new List<MessageColumn>
                    {
                        new MessageColumn
                        {
                            Value = "100", MessageTypeColumn = new MessageTypeColumn
                            {
                                Name = "queueSize",
                                Type = "integer"
                            }
                        }
                    }
                }
            }
        };


        [Theory]
        [MemberData(nameof(ShouldGenerateAlertData))]
        public void ShouldGenerateAlertTest(Rule rule, Message message)
        {
            var ruleEngine = new StandardRuleEngine();
            Assert.NotNull(ruleEngine.ConsumeMessage(rule, message));
        }

        [Theory]
        [MemberData(nameof(ShouldntGenerateAlertData))]
        public void ShouldntGenerateAlertTest(Rule rule, Message message)
        {
            var ruleEngine = new StandardRuleEngine();
            Assert.Null(ruleEngine.ConsumeMessage(rule, message));
        }
    }
}