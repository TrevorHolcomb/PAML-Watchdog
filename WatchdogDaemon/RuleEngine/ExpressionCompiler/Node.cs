namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// An INode is a node in the tree.
    /// </summary>
    public interface INode
    {
        string Evaluate();
    }
}
