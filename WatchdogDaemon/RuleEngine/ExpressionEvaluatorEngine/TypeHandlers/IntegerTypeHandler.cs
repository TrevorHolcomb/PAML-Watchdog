using System;
using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class IntegerTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "Integer";
        }

        public bool IsValid(string value)
        {
            try
            {
                long longTest = System.Int64.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, Int64.Parse(value), typeof(long));
        }
    }
}
