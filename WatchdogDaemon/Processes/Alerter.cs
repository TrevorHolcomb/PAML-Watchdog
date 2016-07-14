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

        private void ProcessMessage(List<Rule> rules, Message message)
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

                    Logger.Info("Generating Alert For Rule '{0}' ", rule.Name);
                }

                Logger.Info("Removing Message Of Type {0}", message.MessageType.Name);
                MessageParameterRepository.DeleteRange(message.MessageParameters.ToList());
                MessageRepository.Delete(message);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Uncaught Exception {0}", e);
            }
        }

        private AlertGroup GetAlertGroup(Rule rule, Message message)
        {
            var alertGroup =  AlertGroupRepository.Get().FirstOrDefault(ag => ag.Server == message.Server && ag.Engine == message.EngineName && ag.Origin == message.Origin && ag.AlertType == rule.AlertType && ag.Resolved == false);

            if(alertGroup == null)
            {
                alertGroup = BuildAlertGroup(rule, message);

                AlertGroupRepository.Insert(alertGroup);
                AlertGroupRepository.Save();
            }

            return alertGroup;

        }

        private Alert BuildAlert(Rule rule, Message message, AlertGroup alertGroup)
        {
            ICollection<AlertParameter> alertParams = new List<AlertParameter>();

            var currentStatus = new AlertStatus
            {
                TimeStamp = System.DateTime.Now,
                ModifiedBy = "Watchdog Daemon",
                StatusCode = StatusCode.UnAcknowledged,
                Next = null,
                Prev = null,
            };


            foreach (var messageParameter in message.MessageParameters)
            {
                alertParams.Add(new AlertParameter
                {
                    Value = messageParameter.Value,
                    MessageTypeParameterType = messageParameter.MessageTypeParameterType,
                });
            }

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
                AlertGroup = alertGroup
            };
        }

        private AlertGroup BuildAlertGroup(Rule rule, Message message)
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
