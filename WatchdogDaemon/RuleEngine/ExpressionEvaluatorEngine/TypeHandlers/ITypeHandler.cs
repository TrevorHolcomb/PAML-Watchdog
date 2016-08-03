using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    /// <summary>
    /// An ITypeHandler binds a message parameter to the scope for a CompileExpression.
    /// </summary>
    internal interface ITypeHandler
    {
        /// <summary>
        /// Returns the name of the TypeHandler. This is used to match a TypeHandler to a message parameter.
        /// </summary>
        string GetTypeName();

        /// <summary>
        /// RegisterValue registers a given parameter to scope (TypeRegistry) for a CompiledExpression to evaluate.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The String representation of the parameter's value.</param>
        /// <param name="registry">The scope that the parameter is being bound to.</param>
        void RegisterValue(string name, string value, TypeRegistry registry);

        /// <summary>
        /// Returns true if the given String representation of a parameter is valid.
        /// </summary>
        /// <param name="value">The parameter value to be tested.</param>
        bool IsValid(string value);
    }
}
