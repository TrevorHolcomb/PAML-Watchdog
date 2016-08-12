using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluator
{
    /// <summary>
    /// An INode is a node in the tree.
    /// </summary>
    public interface INode
    {
        bool Evaluate(Dictionary<string, MessageParameter> parameters);
    }
}
