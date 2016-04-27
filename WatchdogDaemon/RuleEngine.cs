using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WatchdogDatabaseAccessLayer;
using Newtonsoft.Json;


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

        //assuming input is good since it should be validated by here, and validating here would be slow
        public override void ConsumeMessage(Rule rule, Message message)
        {
            //convert the JSON into operator and operands
            String ruleJson = rule.RuleTrigger;
            Console.WriteLine("JSON rule: " + ruleJson);
            String theOperator, leftOperand, rightOperand;
            GetTriggerOperation(ruleJson, out theOperator, out leftOperand, out rightOperand);
                //could validate the variables are filled out here


            //search for param matching left operand in message params
            string leftOperandValue = FindValueOfMatchingKey(message.Params, leftOperand);
                //could verify that leftOperandValue != null

            //perform the operation
            Boolean ruleViolated = CheckForRuleViolation(theOperator, leftOperandValue, rightOperand);
            if (ruleViolated)
                CreateAlert(rule, message);
        }

        private void GetTriggerOperation(string jsonRule, out string theOperator, out string leftOperand, out string rightOperand)
        {
            JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(jsonRule));

            jsonTextReader.Read();                                  //{
            jsonTextReader.Read();                                  //"exampleOperator":
            theOperator = jsonTextReader.Value.ToString();
            jsonTextReader.Read();                                  //      {
            jsonTextReader.Read();                                  //          "exampleVariableName":
            leftOperand = jsonTextReader.Value.ToString();
            jsonTextReader.Read();                                  //                                "exampleVariableValue"
            rightOperand = jsonTextReader.Value.ToString();

            jsonTextReader.Close();

        }

        private string FindValueOfMatchingKey(string jsonToSearch, string targetKey)
        {
            JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(jsonToSearch));
            while (jsonTextReader.Read())
            {
                if (jsonTextReader.Value != null && jsonTextReader.Value.Equals(targetKey))
                {
                    jsonTextReader.Read();
                    return jsonTextReader.Value.ToString();
                }
            }
            return null;
        }

        private Boolean CheckForRuleViolation(string operation, string leftOperand, string rightOperand)
        {
            Func<String, String, Boolean> operationToDo;
            switch (operation)
            {
                case "eq":
                    operationToDo = (l, r) => l.Equals(r);
                    break;
                case "lt":
                    operationToDo = (l, r) => l.CompareTo(r) < 0;
                    break;
                case "gt":
                    operationToDo = (l, r) => l.CompareTo(r) > 0;
                    break;
                case "regex":
                    operationToDo = (l, r) => new Regex(l).Match(r).Success;
                    break;
                default:
                    operationToDo = (l, r) => false;
                    break;
            }
            return operationToDo(leftOperand, rightOperand);
        }

        //TODO: CreateAlert: increment and set alert ID, set correct AlertTypeId, Payload and AlertStatusId
        private void CreateAlert(Rule rule, Message message)
        {
            Random random = new Random();
            Alert newAlert = new Alert
            {
                Id = random.Next(),                             
                AlertTypeId = 0,                                
                RuleId = rule.Id,
                Payload = "nuclear warhead",                    
                Timestamp = DateTime.Now,
                AlertStatusId = 0
            };
            
            dbContext.Alerts.Add(newAlert); //TODO: figure out how to make this change persist when using Moq
            dbContext.SaveChanges();
        }
    }
}