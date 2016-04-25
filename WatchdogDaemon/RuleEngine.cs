using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer;

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
                message.Processed = true;

                //for debugging/simulation
                Console.WriteLine("Consumed: " + message.Id);
            }
        }

        public override void ConsumeMessage(Rule rule, Message message)
        {
            //TODO: use fancy JSON comparator John mentioned
            //if (rule.RuleTrigger == /*triggered*/)
            //create a corresponding alert
            //foreach (Alert alert in rule.Alerts)
            //alert.Triggered = true;                   

            /*TODO: discuss implementation of alert activation. 
                We should find out what the Notifier expects as input. 
                But if we were to redesign that too, I'd propose adding a 
                boolean field, Alerts.Active, analogous to Messages.Processed
            */
        }
    }
}