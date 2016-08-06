using System;
using WatchdogDaemon.RuleEngine.ExpressionCompiler;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class ExpressionCompilerTests
    {
        public static TheoryData<string> ShouldntThrowError = new TheoryData<string>
        {
            @"
{
  ""condition"": ""OR"",
  ""rules"": [
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""equal"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""not_equal"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""in"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""not_in"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""begins_with"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""not_begins_with"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""contains"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""not_contains"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""ends_with"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""not_ends_with"",
      ""value"": ""foo""
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""is_empty"",
      ""value"": null
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""is_not_empty"",
      ""value"": null
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""is_null"",
      ""value"": null
    },
    {
      ""id"": ""exception"",
      ""field"": ""exception"",
      ""type"": ""string"",
      ""input"": ""text"",
      ""operator"": ""is_not_null"",
      ""value"": null
    }
  ]
}",
            @"
{
  ""condition"": ""OR"",
  ""rules"": [
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""equal"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""not_equal"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""in"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""not_in"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""less"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""less_or_equal"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""greater"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""greater_or_equal"",
      ""value"": ""1""
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""between"",
      ""value"": [
        ""1"",
        ""2""
      ]
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""not_between"",
      ""value"": [
        ""1"",
        ""2""
      ]
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""is_null"",
      ""value"": null
    },
    {
      ""id"": ""queue_size"",
      ""field"": ""queue_size"",
      ""type"": ""integer"",
      ""input"": ""text"",
      ""operator"": ""is_not_null"",
      ""value"": null
    }
  ]
}
"
        };

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
        public void TestCorrectExpressionBuilt(string expression, string expected)
        {
            Assert.Equal(expected, Compiler.Convert(expression));
        }

        [Theory]
        [MemberData(nameof(ShouldntThrowError))]
        public void TestAllString(string expression)
        {
            Compiler.Convert(expression);
        }
    }
}
