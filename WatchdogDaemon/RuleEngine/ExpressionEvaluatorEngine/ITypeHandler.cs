using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine
{
    internal interface ITypeHandler
    {
        string GetTypeName();
        void RegisterValue(string name, string value, TypeRegistry registry);
    }
}
