using System;
using System.Linq;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer;
using NJsonSchema;

namespace WatchdogDaemon
{
    public class RuleEngine : IRuleEngine
    {

        public override void ConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages)
        {
            ICollection<Message> unprocessedMessages = messages.Where(message => !message.IsProcessed).ToList();
            foreach (Message message in unprocessedMessages)
            {
                foreach (var rule in rules)
                {
                    //TODO: for some future commit, play with storing Rule.MessageTypeAs Json to allow
                    //validating one rule against many message types
                    if ((rule.Origin == "all" || rule.Origin == message.Origin) && (
                        rule.Server == "all" || rule.Server == message.Server) && (
                        rule.MessageType == null || rule.MessageType.Id == message.MessageType.Id))
                    {
                        ConsumeMessage(rule, message);
                    }
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

            if (result.Count != 0)
                CreateAlert(rule, message);
        }

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
    }
}