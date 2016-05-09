using System.Collections.Generic;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class TypeHandlerList
    {
        public static readonly IEnumerable<ITypeHandler> TypeHandlers = new List<ITypeHandler>
        {
            new DoubleTypeHandler(),
            new IntegerTypeHandler()
        };
    }
}
