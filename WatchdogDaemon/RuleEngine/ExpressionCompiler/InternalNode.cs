using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// An internal node in the tree. Contains a list of rules, and a condition to apply to them (AND or OR)
    /// </summary>
    public class InternalNode : INode
    {
        public ICollection<INode> Nodes { get; set; }
        private string _condition;
        public string Condition
        {
            get
            {
                switch (_condition)
                {
                    case "AND":
                        return " && ";
                    case "OR":
                        return " || ";
                    default:
                        throw new Exception("Invalid Condition: " + _condition);
                }
            }
            set { _condition = value.ToUpper(); }
        }

        /// <summary>
        /// The constructor will create the node from the given token, and create all of its children at the same time.
        /// </summary>
        /// <param name="token"></param>
        public InternalNode(JToken token)
        {
            Nodes = new List<INode>();

            var rules = token.SelectToken("rules");

            _condition = token.SelectToken("condition").ToString();

            foreach (var rule in rules)
            {
                if (IsTokenInternal(rule))
                {
                    Nodes.Add(new InternalNode(rule));
                }
                else if (IsLeafNullary(rule))
                {
                    Nodes.Add(rule.ToObject<NullaryLeaf>());
                }
                else if (IsLeafUnary(rule))
                {
                    Nodes.Add(rule.ToObject<UnaryLeaf>());
                }
                else if (IsLeafPolyadic(rule))
                {
                    Nodes.Add(rule.ToObject<PolyadicLeaf>());
                }
                else
                {
                    throw new Exception("Invalid Leaf Value");
                }
            }
        }

        /// <summary>
        /// Generates the expression from this node, and all of its children nodes.
        /// </summary>
        /// <returns>The generated expression.</returns>
        public string Evaluate()
        {
            return "(" + string.Join(Condition, Nodes.Select(e => e.Evaluate())) + ")";
        }

        /// <summary>
        /// Returns true if the current node that the token holds is an internal node.
        /// </summary>
        /// <param name="token">The token representing the node being checked.</param>
        /// <returns>True if node is internal, false if leaf.</returns>
        private static bool IsTokenInternal(JToken token)
        {
            var rules = token.SelectToken("rules");

            if (rules == null || !rules.Any())
                return false;

            return rules.Type == JTokenType.Array;

        }

        /// <summary>
        /// Returns true if the current node is a nulllary node. ( if the value property is null )
        /// </summary>
        /// <param name="token">The token representing the node being checked.</param>
        /// <returns>True if the node's value property is null.</returns>
        private static bool IsLeafNullary(JToken token)
        {
            return token.SelectToken("value") == null;
        }

        /// <summary>
        /// Returns true if the current node is a unary leaf node. ( if the value property is a string )
        /// </summary>
        /// <param name="token">The token representing the node being checked.</param>
        /// <returns>True if node's value property is a string.</returns>
        private static bool IsLeafUnary(JToken token)
        {
            var values = token.SelectToken("value");
            return values.Type == JTokenType.String;
        }

        /// <summary>
        /// Returns true if the current node is a polyadic leaf node. ( if the value property is an array )
        /// </summary>
        /// <param name="token">The token representing the node being checked.</param>
        /// <returns>True if node's value property is an array.</returns>
        private static bool IsLeafPolyadic(JToken token)
        {
            var values = token.SelectToken("value");
            return values.Type == JTokenType.Array;
        }
    }
}
