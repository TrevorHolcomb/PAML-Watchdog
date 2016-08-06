using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NLog;
using WatchdogDaemon.RuleEngine;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace WatchdogDaemon.Processes
{
    /// <summary>
    /// The Alerter reads messages and rules from the database and evaluates the messages using the rules. If the rule evaluates to true then it creates a new alert.
    /// </summary>
    public class Alerter : IProcess
    {
        [Inject]
        public Repository<Alert> AlertRepository { set; get; }
        [Inject]
        public Repository<Message> MessageRepository { set; get; }
        [Inject]
        public Repository<MessageParameter> MessageParameterRepository { set; get; }
        [Inject]
        public Repository<Rule> RuleRepository { set; get; }
        [Inject]
        public Repository<UnvalidatedMessage> UnvalidatedMessageRepository { set; get; }
        [Inject]
        public Repository<AlertParameter> AlertParameterRepository { set; get; }
        [Inject]
        public Repository<AlertStatus> AlertStatusRepository { set; get; }
        [Inject]
        public Repository<AlertGroup> AlertGroupRepository { set; get; }
        [Inject]
        public IRuleEngine RuleEngine { set; get; }
        [Inject]
        public Repository<AlertCategoryItem> AlertCategoryItemRepository { set; get; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Dispose()
        {
            MessageRepository.Dispose();
        }

        public void Run()
        {

            //run rule engine
            var messages = MessageRepository.Get().ToList();
            var rules = RuleRepository.Get().ToList();

            foreach (var message in messages)
            {
                ProcessMessage(rules, message);
            }

            MessageParameterRepository.Save();
            MessageRepository.Save();
            AlertRepository.Save();
        }

        private void ProcessMessage(IEnumerable<Rule> rules, Message message)
        {
            try
            {
                foreach (var rule in rules)
                {
                    if (rule.MessageType != message.MessageType)
                        continue;

                    if (!RuleEngine.DoesGenerateAlert(rule, message))
                        continue;

                    var alertGroup = GetAlertGroup(rule, message);                      
                    var alert = BuildAlert(rule, message, alertGroup);
                    AlertRepository.Insert(alert);
                    AlertStatusRepository.Insert(alert.AlertStatus);
                    AlertParameterRepository.InsertRange(alert.AlertParameters);

                    AlertRepository.Save();
                    AlertStatusRepository.Save();
                    AlertParameterRepository.Save();

                    Logger.Info($"Generating Alert For Rule '{rule.Name}' ");
                }

                Logger.Info($"Removing Message Of Type {message.MessageType.Name}");
                MessageParameterRepository.DeleteRange(message.MessageParameters.ToList());
                MessageRepository.Delete(message);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Uncaught Exception {e}");
            }
        }

        private AlertGroup GetAlertGroup(Rule rule, Message message)
        {
            var alertGroup =  AlertGroupRepository.Get().FirstOrDefault(ag => ag.Server == message.Server && ag.Engine == message.EngineName && ag.Origin == message.Origin && ag.AlertType == rule.AlertType && ag.Resolved == false);
            var alertCategory = AlertCategoryItemRepository.Get().FirstOrDefault(ag => ag.Server == message.Server && ag.Engine == message.EngineName && ag.Origin == message.Origin && ag.AlertType == rule.AlertType);
            if (alertGroup == null)
            {
                alertGroup = BuildAlertGroup(rule, message);
                if (alertCategory != null)
                    alertGroup.AlertCategoryId = alertCategory.AlertCategoryId;
                AlertGroupRepository.Insert(alertGroup);
                AlertGroupRepository.Save();
            }

            return alertGroup;

        }

        private static Alert BuildAlert(Rule rule, Message message, AlertGroup alertGroup)
        {
            var currentStatus = new AlertStatus
            {
                Timestamp = System.DateTime.Now,
                ModifiedBy = "Watchdog Daemon",
                StatusCode = StatusCode.UnAcknowledged,
                Next = null,
                Prev = null,
            };


            ICollection<AlertParameter> alertParams = message.MessageParameters.Select(messageParameter => new AlertParameter
            {
                Value = messageParameter.Value, MessageTypeParameterType = messageParameter.MessageTypeParameterType,
            }).ToList();

            string notes = "";

            if(rule.DefaultNotes != null)
            {
                foreach(DefaultNote note in rule.DefaultNotes)
                {
                    notes += note.Text + " ";
                }
            }
            
            return new Alert
            {
                AlertParameters = alertParams,
                AlertType = rule.AlertType,
                AlertTypeId = rule.AlertTypeId,
                Engine = message.Engine,
                Notes = notes,
                Origin = message.Origin,
                Rule = rule,
                RuleId = rule.Id,
                Server = message.Server,
                Severity = rule.DefaultSeverity,
                AlertStatus = currentStatus,
                MessageType = message.MessageType,
                AlertGroup = alertGroup
            };
        }

        private static AlertGroup BuildAlertGroup(Rule rule, Message message)
        {
            return new AlertGroup 
            {
                Server = message.Server,
                Engine = message.EngineName,
                Origin = message.Origin,
                AlertType = rule.AlertType,
                Resolved = false
            };
        }
    }
}
