using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class IntegerTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "integer";
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            registry.RegisterSymbol(name, int.Parse(value), typeof(int));
        }
    }
}
