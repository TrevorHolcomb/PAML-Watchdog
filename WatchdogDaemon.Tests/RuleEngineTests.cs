using System.Collections.Generic;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.TreeEngine;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        public static TheoryData<IRuleEngine, Rule, IEnumerable<Message>> ShouldGenerateAlertData = new TheoryData<IRuleEngine, Rule, IEnumerable<Message>>
        {
            {
                new TreeExpressionEvaluator(), 
                new Rule
                {
                    Expression = 
@"{
    condition:""AND"",
    rules:[
        {
            ""id"":""queueSize"",
            ""operator"":""greater"",
            ""value"":""100"",
            ""type"":""integer""
        }
    ]
}"
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
            },
            {
                new TreeExpressionEvaluator(),
                new Rule
                {
                    Expression =
@"{
    condition:""AND"",
    rules:[
        {
            ""id"":""queue_name"",
            ""operator"":""equal"",
            ""value"":""foo"",
            ""type"":""string""
        }
    ]
}"
                },
                new List<Message> {
                    new Message
                    {
                        MessageParameters = new List<MessageParameter>
                        {
                            new MessageParameter
                            {
                                Value = "foo", MessageTypeParameterType = new MessageTypeParameterType
                                {
                                    Name = "queue_name",
                                    Type = "string"
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
                new TreeExpressionEvaluator(),
                new Rule
                {
                    Expression = 
@"{
    condition:""AND"",
    rules:[
        {
            ""id"":""queueSize"",
            ""operator"":""greater"",
            ""value"":""1000"",
            ""type"":""integer""
        }
    ]
}"
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
                                Value = "1000", MessageTypeParameterType = new MessageTypeParameterType
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
                Assert.True(ruleEngine.Evaluate(rule, message));
        }

        [Theory]
        [MemberData(nameof(ShouldntGenerateAlertData))]
        public void ShouldntGenerateAlertTest(IRuleEngine ruleEngine, Rule rule, IEnumerable<Message> messages)
        {
            foreach(var message in messages)
                Assert.False(ruleEngine.Evaluate(rule, message));
        }
    }
}