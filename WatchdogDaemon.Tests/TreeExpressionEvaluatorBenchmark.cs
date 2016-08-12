using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xunit.Performance;
using WatchdogDaemon.RuleEngine.TreeEngine;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{
    public class TreeExpressionEvaluatorBenchmark
    {
        public static Rule Rule = new Rule
        {
            Expression = @"{
              ""condition"": ""AND"",
              ""rules"": [
                {
                  ""id"": ""queue_size"",
                  ""field"": ""queue_size"",
                  ""type"": ""integer"",
                  ""input"": ""text"",
                  ""operator"": ""greater"",
                  ""value"": ""10""
                }
              ]
            }"
        };

        public static Message Message = new Message()
        {
            MessageParameters = new List<MessageParameter>()
            {
                new MessageParameter()
                {
                    Value = "100",
                    MessageTypeParameterType = new MessageTypeParameterType
                    {
                        Name = "queue_size",
                        Type = "integer"
                    }
                }
            }
        };

        public static TreeExpressionEvaluator Evaluator = new TreeExpressionEvaluator();

        [Benchmark(InnerIterationCount = 10000)]
        public void BenchmarkEvaluation()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for(var i = 0; i < Benchmark.InnerIterationCount; i++)
                        Evaluator.Evaluate(Rule, Message);
                }
            }
        }
    }
}
