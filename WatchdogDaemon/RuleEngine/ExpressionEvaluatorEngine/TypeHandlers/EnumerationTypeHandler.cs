using System;
using ExpressionEvaluator;


//TODO - Enum will return objects for now
namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class EnumerationTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "Enumeration";
        }

        public bool IsValid(string value)
        {
            try
            {
                Object parsedEnum = Enum.Parse(typeof(string),value);
                if (parsedEnum == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, Enum.Parse(typeof(string), value), typeof(Object));
        }
    }
}