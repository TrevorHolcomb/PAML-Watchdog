using System.Collections.Generic;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    /// <summary>
    /// Contains the list of all the TypeHandlers to be used.
    /// </summary>
    public class TypeHandlerList
    {
        public static readonly IEnumerable<ITypeHandler> TypeHandlers = new List<ITypeHandler>
        {
            new DecimalTypeHandler(),
            new IntegerTypeHandler(),
            new BooleanTypeHandler(),
            new DateTimeTypeHandler(),
            new EnumerationTypeHandler(),
            new ExceptionTypeHandler()
        };
    }
}
