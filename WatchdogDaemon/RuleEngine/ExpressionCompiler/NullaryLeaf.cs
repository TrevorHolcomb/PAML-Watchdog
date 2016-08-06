using WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// A leaf in the expression tree that has no parameters.
    /// </summary>
    public class NullaryLeaf : INode
    {
        public string Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Input { get; set; }
        public string Operator { get; set; }
        public string[] Value { get; set; }
        public string Evaluate()
        {
            return TypeHandlerList.BuildExpression(Type, Id, Operator);
        }
    }
}