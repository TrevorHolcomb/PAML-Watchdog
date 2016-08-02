using System.Collections.Generic;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        public static TheoryData<IRuleEngine, Rule, IEnumerable<Message>> ShouldGenerateAlertData = new TheoryData<IRuleEngine, Rule, IEnumerable<Message>>
        {
            {
                new StandardRuleEngine(),
                new Rule
                {
                    Expression = "queueSize > 1000"
                },
                new List<Message> {
                    new Message
                    {
                        MessageParameters = new List<MessageParameter>
                        {
                            new MessageParameter
                            {
                                Value = "10000", MessageTypeParameterType = new MessageTypeParameterType
                                {
                                    Name = "queueSize",
                                    Type = "Integer"
                                }
                            }
                        }
                    },
                        new Message
                    {
                        MessageParameters = new List<MessageParameter>
                        {
                            new MessageParameter
                            {
                                Value = "19900", MessageTypeParameterType = new MessageTypeParameterType
                                {
                                    Name = "queueSize",
                                    Type = "Integer"
                                }
                            }
                        }
                    }
                }
            }
        };

        public static TheoryData<IRuleEngine, Rule, IEnumerable<Message>> ShouldntGenerateAlertData = new TheoryData<IRuleEngine, Rule, IEnumerable<Message>>
        {
            {
                new StandardRuleEngine(),
                new Rule
                {
                    Expression = "queueSize > 1000"
                },
                new List<Message> {
                    new Message
                    {
                        MessageParameters = new List<MessageParameter>
                        {
                            new MessageParameter
                            {
                                Value = "100", MessageTypeParameterType = new MessageTypeParameterType
                                {
                                    Name = "queueSize",
                                    Type = "Integer"
                                }
                            },
                        }
                    }, new Message
                    {
                        MessageParameters = new List<MessageParameter>
                        {
                            new MessageParameter
                            {
                                Value = "100", MessageTypeParameterType = new MessageTypeParameterType
                                {
                                    Name = "queueSize",
                                    Type = "Integer"
                                }
                            },
                        }
                    },
                }
            }
        };


        [Theory]
        [MemberData(nameof(ShouldGenerateAlertData))]
        public void ShouldGenerateAlertTest(IRuleEngine ruleEngine, Rule rule, IEnumerable<Message> messages)
        {
            foreach(var message in messages)
                Assert.True(ruleEngine.DoesGenerateAlert(rule, message));
        }

        [Theory]
        [MemberData(nameof(ShouldntGenerateAlertData))]
        public void ShouldntGenerateAlertTest(IRuleEngine ruleEngine, Rule rule, IEnumerable<Message> messages)
        {
            foreach(var message in messages)
                Assert.False(ruleEngine.DoesGenerateAlert(rule, message));
        }
    }
}