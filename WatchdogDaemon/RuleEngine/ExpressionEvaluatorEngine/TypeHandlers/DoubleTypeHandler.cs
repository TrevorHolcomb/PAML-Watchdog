using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class DoubleTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "double";
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, double.Parse(value), typeof(double));
        }
    }
}
