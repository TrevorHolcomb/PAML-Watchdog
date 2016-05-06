using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer;
using NJsonSchema;

namespace WatchdogDaemon
{
    public class RuleEngine : IRuleEngine
    {

        public override ICollection<Alert> ConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            var alerts = new List<Alert>();
            foreach (var message in messages)
            {
                foreach (var rule in rules)
                {
                    //if (!(rule.Server == message.Server && rule.Origin == message.Origin))
                    //continue;

                    //if (rule.MessageType.Id != message.MessageTypeId)
                        //continue;

                    var alert = ConsumeMessage(rule, message);
                    if (alert != null)
                    {
                        alerts.Add(alert);
                        Console.WriteLine("Created Alert For: " + message.Id);
                    }
                }
                message.IsProcessed = true;

                //for debugging/simulation
                Console.WriteLine("Consumed: " + message.Id);
            }
            return alerts;
        }

        public override Alert ConsumeMessage(Rule rule, Message message)
        {
            //Manatee.Json doesn't properly support "exclusiveMaximum" somehow. Yet, it does support"exclusiveMinimum".
            //IJsonSchema ruleTriggerSchema = JsonSchemaFactory.FromJson(JsonValue.Parse(rule.RuleTrigger));        

            JsonSchema4 ruleTriggerSchema = JsonSchema4.FromJson(rule.RuleTriggerSchema);
            var result = ruleTriggerSchema.Validate(message.Params);

            if (result.Count != 0)
                return CreateAlert(rule, message);

            return null;
        }

        #region private methods

        private static Alert CreateAlert(Rule rule, Message message)
        {
            return new Alert
            {
                AlertTypeId = rule.AlertTypeId,
                AlertType = rule.AlertType,
                RuleId = rule.Id,
                Rule = rule,
                Payload = message.Params,
                Notes = "",
                Timestamp = DateTime.Now,
                Status = (int)AlertStatus.UnAcknowledged,
            };
        }
       
        #endregion
    }
}