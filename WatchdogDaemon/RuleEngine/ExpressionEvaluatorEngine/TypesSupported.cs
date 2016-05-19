using System;
using System.Collections.Generic;
using System.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine
{
    public static class TypesSupported
    {
        public static List<String> Types = TypeHandlers.TypeHandlerList.TypeHandlers.Select(typeHandler => typeHandler.GetTypeName()).ToList();
    }
}
