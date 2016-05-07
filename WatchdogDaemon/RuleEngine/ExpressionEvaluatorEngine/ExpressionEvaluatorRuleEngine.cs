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
            foreach (var column in message.MessageColumns)
            {
                RegisterMessageParameter(typeHandlers, column, registry);
            }
        }

        private static void RegisterMessageParameter(IEnumerable<ITypeHandler> typeHandlers, MessageColumn column, TypeRegistry registry)
        {
            foreach (var handler in typeHandlers)
            {
                if (column.MessageTypeColumn.Type == handler.GetTypeName())
                {
                    handler.RegisterValue(column.MessageTypeColumn.Name, column.Value, registry);
                    return;
                }
            }

            throw new KeyNotFoundException("Couldn't find TypeHandler for \"" + column.MessageTypeColumn.Type + "\"");
        }

        public Alert ConsumeMessage(Rule rule, Message message)
        {
            var registry = RegisterVariables(message);
            var ce = new CompiledExpression(rule.Expression) {TypeRegistry = registry};
            var triggers = ce.Eval() is bool && (bool) ce.Eval();

            if (triggers)
                return CreateAlert(rule, message);
            return null;
        }

        private static Alert CreateAlert(Rule rule, Message message)
        {
            return new Alert
            {
                AlertType = rule.AlertType,
                AlertTypeId = rule.AlertTypeId,
                Rule = rule,
                RuleId = rule.Id
            };
        }


    }
}