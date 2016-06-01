using System;
using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class IntegerTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "integer";
        }

        public bool IsValid(string value)
        {
            throw new NotImplementedException();
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, int.Parse(value), typeof(int));
        }
    }
}
