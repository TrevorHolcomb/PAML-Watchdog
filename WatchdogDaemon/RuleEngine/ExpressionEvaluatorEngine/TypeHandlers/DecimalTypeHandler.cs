using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class DecimalTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "Decimal";
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, double.Parse(value), typeof(double));
        }

        public bool IsValid(string value)
        {
            try
            {
                double doubleTest = System.Double.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
