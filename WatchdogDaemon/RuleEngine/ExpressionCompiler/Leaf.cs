using WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// The leafnode in the expression tree.
    /// </summary>
    public class Leaf : INode
    {

        public string Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Input { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Evaluate()
        {
            return TypeHandlerList.BuildExpression(Type, Id, Operator, Value);
        }
    }
}
