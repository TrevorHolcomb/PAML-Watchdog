using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WatchdogDaemon.RuleEngine.ExpressionEvaluator;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine
{
    /// <summary>
    /// An internal node in the tree. Contains a list of rules, and a condition to apply to them (AND or OR)
    /// </summary>
    public class InternalNode : INode
    {
        public ICollection<INode> Nodes { get; set; }
        public string Condition { get; set; }

        /// <summary>
        /// The constructor will create the node from the given token, and create all of its children at the same time.
        /// </summary>
        /// <param name="token"></param>
        public InternalNode(JToken token)
        {
            Nodes = new List<INode>();

            var rules = token.SelectToken("rules");

            Condition = token.SelectToken("condition").ToString();

            foreach (var rule in rules)
            {
                Nodes.Add(TreeEngine.TreeExpressionEvaluator.BuildNode(rule));
            }
        }

        /// <summary>
        /// Evaluates this node, and all of its children recursively.
        /// </summary>
        /// <returns>The generated expression.</returns>
        public bool Evaluate(Dictionary<string, MessageParameter> parameters)
        {
            return Condition.Equals("AND") ? Nodes.All(e => e.Evaluate(parameters)) : Nodes.Any(e => e.Evaluate(parameters));
        }
    }
}
