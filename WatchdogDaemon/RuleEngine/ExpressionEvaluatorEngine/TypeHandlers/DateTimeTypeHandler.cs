using System;
using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class DateTimeTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "DateTime";
        }

        public bool IsValid(string value)
        {
            try
            {
                DateTime dateTimeTest = System.DateTime.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, DateTime.Parse(value), typeof(DateTime));
        }
    }
}