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

        public Alert ConsumeMessage(Rule rule, Message message)
        {
            //TODO: enforce rule constraints on messageType
            //TODO: check if rule.Server, Origin, and Engine are set to 'all', or a specific target
            var registry = RegisterVariables(message);
            var ce = new CompiledExpression(rule.Expression) {TypeRegistry = registry};
            var triggers = ce.Eval() is bool && (bool) ce.Eval();

            if (triggers)
                return CreateAlert(rule, message);
            return null;
        }

        private static Alert CreateAlert(Rule rule, Message message)
        {
            ICollection<AlertParameter> alertParams = new List<AlertParameter>();

            foreach (MessageParameter messageParameter in message.MessageParameters){
                alertParams.Add(new AlertParameter
                {
                    MessageTypeParameterTypeId = messageParameter.MessageTypeParameterTypeId,
                    Value = messageParameter.Value,
                    MessageTypeParameterType = messageParameter.MessageTypeParameterType
                });
            }

            AlertStatus currentStatus = new AlertStatus
            {
                ModifiedAt = System.DateTime.Now,
                ModifiedBy = "Watchdog Daemon",
                StatusCode = StatusCode.UnAcknowledged,
                Next = null,
                Prev = null
            };

            return new Alert
            {
                AlertParameters = alertParams,
                AlertType = rule.AlertType,
                AlertTypeId = rule.AlertTypeId,
                Engine = message.Engine,
                Notes = " ",
                Origin = message.Origin,
                Rule = rule,
                RuleId = rule.Id,
                Server = message.Server,
                Severity = rule.DefaultSeverity,
                AlertStatus = currentStatus,
                MessageType = message.MessageType,
            };
        }
    }
}