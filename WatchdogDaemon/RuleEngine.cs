using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer;
using NJsonSchema;

namespace WatchdogDaemon
{
    public class RuleEngine : IRuleEngine
    {

        public override void ConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            foreach (var message in messages)
            {
                foreach (var rule in rules)
                {
                    ConsumeMessage(rule, message);
                }
                message.IsProcessed = true;

                //for debugging/simulation
                Console.WriteLine("Consumed: " + message.Id);
            }
        }

        public override void ConsumeMessage(Rule rule, Message message)
        {
            //Manatee.Json doesn't properly support "exclusiveMaximum" somehow. Yet, it does support"exclusiveMinimum".
            //IJsonSchema ruleTriggerSchema = JsonSchemaFactory.FromJson(JsonValue.Parse(rule.RuleTrigger));        

            JsonSchema4 ruleTriggerSchema = JsonSchema4.FromJson(rule.RuleTriggerSchema);
            var result = ruleTriggerSchema.Validate(message.Params);

            //here's where we handle matching rules to specific servers and origins
            foreach (var error in result)
                if (error.Property == "server" || error.Property == "origin")       
                    return;

            if (result.Count != 0)
                CreateAlert(rule, message);
        }

        #region private methods

        private void CreateAlert(Rule rule, Message message)
        {
            Alert newAlert = new Alert
            {
                AlertTypeId = rule.AlertTypeId,
                RuleId = rule.Id,
                Payload = message.Params,
                Timestamp = DateTime.Now,
                Status = (int)AlertStatus.UnAcknowledged,
            };

            dbContext.Alerts.Add(newAlert);
            dbContext.SaveChanges();
        }
       
        #endregion
    }
}