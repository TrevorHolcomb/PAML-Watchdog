using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// The compiler is used to compile a valid c# expression from a expression tree stored in a JSON structure.
    /// FROM THIS
    /// {
    ///     "condition":"AND",
    ///     "rules":[
    ///         {
    ///             "id":"exception",
    ///             "field":"exception",
    ///             "type":"string",
    ///             "input":"text",
    ///             "operator":"contains",
    ///             "value":"foo"
    ///         }
    ///     ]
    /// }
    /// 
    /// TO THIS
    /// ((exception.Contains(""foo"")))
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Converts a given JSON expression tree into a C# expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string Convert(string expression)
        {
            var tree = JToken.Parse(expression);
            return BuildNode(tree).Evaluate();
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

        public static INode BuildNode(JToken rule)
        {
            if (IsTokenInternal(rule))
            {
                return (new InternalNode(rule));
            }
            else if (IsLeafNullary(rule))
            {
                return (rule.ToObject<NullaryLeaf>());
            }
            else if (IsLeafUnary(rule))
            {
                return (rule.ToObject<UnaryLeaf>());
            }
            else if (IsLeafPolyadic(rule))
            {
                return (rule.ToObject<PolyadicLeaf>());
            }
            else
            {
                throw new Exception("Invalid Leaf Value");
            }
        }
    }
}
