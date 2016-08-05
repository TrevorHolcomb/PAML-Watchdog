using System;
using System.Collections.Generic;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class ExpressionCompilerTests
    {
        public static TheoryData<string, string> TestData =
            new TheoryData<string, string>
            {
                {
                    @"{
                        ""condition"":""AND"",
                        ""rules"":[
                            {
                                ""id"":""exception"",
                                ""field"":""exception"",
                                ""type"":""string"",
                                ""input"":""text"",
                                ""operator"":""contains"",
                                ""value"":""foo""
                            }
                        ]
                    }",
                    @"((exception.Contains(""foo"")))"
                },
                {
                    @"{
                        ""condition"":""AND"",
                        ""rules"":[
                            {
                                ""id"":""exception"",
                                ""field"":""exception"",
                                ""type"":""string"",
                                ""input"":""text"",
                                ""operator"":""contains"",
                                ""value"":""foo""
                            },
                            {
                                ""id"":""exception"",
                                ""field"":""exception"",
                                ""type"":""string"",
                                ""input"":""text"",
                                ""operator"":""contains"",
                                ""value"":""bar""
                            }
                        ]
                    }",
                    @"((exception.Contains(""foo"")) && (exception.Contains(""bar"")))"
               },

                {
                    @"{
                        ""condition"":""AND"",
                        ""rules"":[
                            {
                                ""id"":""exception"",
                                ""field"":""exception"",
                                ""type"":""string"",
                                ""input"":""text"",
                                ""operator"":""contains"",
                                ""value"":""foo""
                            },
                            {
                                ""id"":""exception"",
                                ""field"":""exception"",
                                ""type"":""string"",
                                ""input"":""text"",
                                ""operator"":""contains"",
                                ""value"":""bar""
                            },
                            {
                                ""condition"":""AND"",
                                ""rules"":[
                                    {
                                        ""id"":""exception"",
                                        ""field"":""exception"",
                                        ""type"":""string"",
                                        ""input"":""text"",
                                        ""operator"":""not_equal"",
                                        ""value"":""foobar""
                                    }
                                ]
                            }
                        ]
                    }",
                    @"((exception.Contains(""foo"")) && (exception.Contains(""bar"")) && ((!exception.Equals(""foobar""))))"
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestMethod1(string expression, string expected)
        {
            Assert.Equal(expected, ExpressionCompiler.Convert(expression));
        }
    }
}
