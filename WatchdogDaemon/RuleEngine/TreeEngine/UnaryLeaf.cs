﻿using System.Collections.Generic;
using WatchdogDaemon.RuleEngine.ExpressionEvaluator;
using WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine
{
    /// <summary>
    /// A leaf in the expression tree that has a single value.
    /// </summary>
    public class UnaryLeaf : INode
    {

        public string Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Input { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public bool Evaluate(Dictionary<string, MessageParameter> parameters)
        {
            return TypeHandlerList.BuildExpression(Type, Id, Operator, Value, parameters);
        }
    }
}