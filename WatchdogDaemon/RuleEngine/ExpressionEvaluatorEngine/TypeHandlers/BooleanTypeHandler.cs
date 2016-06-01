using System;
using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class BooleanTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "Boolean";
        }

        public bool IsValid(string value)
        {
            try
            {
                bool boolTest = System.Boolean.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, bool.Parse(value), typeof(bool));
        }
    }
}