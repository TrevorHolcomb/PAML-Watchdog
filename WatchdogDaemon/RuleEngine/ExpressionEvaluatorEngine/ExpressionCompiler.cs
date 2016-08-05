using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEvaluator;
using Newtonsoft.Json.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine
{
    public class ExpressionCompiler
    {
        public interface INode
        {
            string Evaluate();
        }

        protected class InternalNode : INode
        {
            public InternalNode(JToken token)
            {
                Nodes = new List<INode>();

                var rules = token.SelectToken("rules");

                _condition = token.SelectToken("condition").ToString();

                foreach (var rule in rules)
                {
                    if (isTokenInternal(rule))
                    {
                        Nodes.Add(new InternalNode(rule));
                    }
                    else
                    {
                        Nodes.Add(rule.ToObject<Leaf>());
                    }
                }
            }

            private string _condition;
            public string Condition
            {
                get
                {
                    switch (_condition)
                    {
                        case "AND":
                            return "&&";
                        case "OR":
                            return "||";
                        default:
                            throw new Exception("Invalid Condition: " + _condition);
                    }
                }
                set { _condition = value.ToUpper(); }
            }

            public ICollection<INode> Nodes { get; set; }
            public string Evaluate()
            {
                return "(" + string.Join(Condition, Nodes.Select(e => e.Evaluate())) + ")";
            }
        }

        protected class Leaf : INode
        {
            public string Id { get; set; }
            public string Field { get; set; }
            public string Type { get; set; }
            public string Input { get; set; }
            public string Operator { get; set; }
            public string Value { get; set; }
            public string Evaluate()
            {
                return "()";
            }            
        }

        public static string Convert(string expression)
        {
            var tree = JToken.Parse(expression);
            return new InternalNode(tree).Evaluate();
        }

        private static bool isTokenInternal(JToken token)
        {
            var rules = token.SelectToken("rules");

            if (rules == null || rules.Count() == 0)
                return false;

            return rules.Type == JTokenType.Array;

        }
            
    }
}
