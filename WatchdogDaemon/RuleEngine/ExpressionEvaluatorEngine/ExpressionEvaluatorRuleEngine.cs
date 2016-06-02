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
        private static TypeRegistry RegisterVariables(Message message)
        {
            var registry = new TypeRegistry();
            var typeHandlers = TypeHandlerList.TypeHandlers;

            RegisterMessageParameters(message, typeHandlers, registry);

            return registry;
        }

        private static void RegisterMessageParameters(Message message, IEnumerable<ITypeHandler> typeHandlers, TypeRegistry registry)
        {
            foreach (var parameter in message.MessageParameters)
            {
                RegisterMessageParameter(parameter, typeHandlers, registry);
            }
        }

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

        public bool DoesGenerateAlert(Rule rule, Message message)
        {
            //TODO: check if rule.Server, Origin, and Engine are set to 'all', or a specific target
            var registry = RegisterVariables(message);
            var ce = new CompiledExpression(rule.Expression) {TypeRegistry = registry};
            return ce.Eval() is bool && (bool)ce.Eval();
        }
    }
}