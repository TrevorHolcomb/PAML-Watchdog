using System.Collections;
using System.Collections.Generic;
using ExpressionEvaluator;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine
{
    /// <summary>
    /// This class checks if a Message's parameters will pass a Rule's expression.
    /// </summary>
    public class StandardRuleEngine : IRuleEngine
    {
        private readonly Hashtable _compiledExpressions;

        public StandardRuleEngine()
        {
            _compiledExpressions = new Hashtable();
        }

        private static TypeRegistry RegisterVariables(Message message)
        {
            var registry = new TypeRegistry();
            var typeHandlers = TypeHandlerList.TypeHandlers;

            RegisterMessageParameters(message, typeHandlers, registry);

            return registry;
        }

        /// <summary>
        /// Registers the variables in a message to the evaluator's scope.
        /// </summary>
        /// <param name="message">The message who's parameters are being bound to scope.</param>
        /// <param name="typeHandlers">The list of type handlers to handle type conversion.</param>
        /// <param name="registry">The scope of the expression.</param>
        private static void RegisterMessageParameters(Message message, IEnumerable<ITypeHandler> typeHandlers, TypeRegistry registry)
        {
            foreach (var parameter in message.MessageParameters)
            {
                RegisterMessageParameter(parameter, typeHandlers, registry);
            }
        }

        /// <summary>
        /// Converts a message's parameter to somthing evaluatable by the expression evaluator and binds in to scope.
        /// </summary>
        /// <param name="parameter">The parameter to be bound</param>
        /// <param name="typeHandlers">The type handlers used to handle type conversion.</param>
        /// <param name="registry">The scope of the expression.</param>
        private static void RegisterMessageParameter(MessageParameter parameter, IEnumerable<ITypeHandler> typeHandlers, TypeRegistry registry)
        {
            foreach (var handler in typeHandlers)
            {
                if (parameter.MessageTypeParameterType.Type == handler.GetTypeName())
                {
                    handler.RegisterValue(parameter.MessageTypeParameterType.Name, parameter.Value, registry);
                    return;
                }
            }

            throw new KeyNotFoundException("Couldn't find TypeHandler for \"" + parameter.MessageTypeParameterType.Type + "\"");
        }

        /// <summary>
        /// Inspects a message evaluates an expression and returns true if it should generate an alert.
        /// </summary>
        /// <param name="rule">The rule being applied.</param>
        /// <param name="message">The message that is being inspected.</param>
        /// <returns>returns true if the Rule evaluates true for this message.</returns>
        public bool DoesGenerateAlert(Rule rule, Message message)
        {
            //TODO: check if rule.Server, Origin, and Engine are set to 'all', or a specific target
            var registry = RegisterVariables(message);
            var ce = GetCompiledExpression(rule.Expression);
            ce.TypeRegistry = registry;
            return ce.Eval() is bool && (bool)ce.Eval();
        }

        /// <summary>
        /// Returns the CompiledExpression pertaining to an Expression. Stores the CompiledExpression in a hashmap for performance.
        /// </summary>
        /// <param name="expression">The string representation of the CompiledExpression being retrieved</param>
        /// <returns>Returns the CompiledExpression for a given string expression.</returns>
        public CompiledExpression GetCompiledExpression(string expression)
        {
            if (_compiledExpressions.ContainsKey(expression))
            {
                return _compiledExpressions[expression] as CompiledExpression;
            }

            var compiledExpression = RuleEngine.ExpressionCompiler.Compiler.Convert(expression);
            var ce = new CompiledExpression(compiledExpression);
            _compiledExpressions.Add(expression, ce);

            return ce;
        }
    }
}