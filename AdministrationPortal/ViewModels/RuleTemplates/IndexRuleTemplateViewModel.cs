using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class IndexRuleTemplateViewModel : IndexViewModel
    {
        public IEnumerable<RuleTemplate> RuleTemplates { get; set; }
        private readonly int? _numberOfRulesInstantiated;
        private readonly string _enginesUsed;
        private readonly string _originsUsed;
        private readonly string _serversUsed;
        public string Timestamp;

        public RuleTemplate RuleTemplateInstantiated { get; set; } = new RuleTemplate()
        {
            TemplatedRules = new List<TemplatedRule>()
        };

        public IndexRuleTemplateViewModel(ActionType action, string entityName = "",
            int? numberOfRulesInstantiated = null, string enginesUsed = "",
            string originsUsed = "", string serversUsed = "", string timestamp = "",
            string message = "")
            : base(action, "Rule Template", entityName, message)
        {
            _numberOfRulesInstantiated = numberOfRulesInstantiated;
            _enginesUsed = enginesUsed;
            _originsUsed = originsUsed;
            _serversUsed = serversUsed;
            Timestamp = timestamp;
        }

        public override MvcHtmlString GetMessage()
        {
            if (ConfigurationManager.AppSettings["InfoMessagesEnabled"] == bool.FalseString ||
                Action == null || Action == ActionType.None)
                return new MvcHtmlString("");

            if (_numberOfRulesInstantiated != null)
            {
                return new MvcHtmlString($"<div class=\"alert alert-success\" role=\"alert\" id=\"divAlert\">" +
                    $"{_numberOfRulesInstantiated} Rules were instantiated with" +
                    $"<b> {RuleTemplateInstantiated.Name} </b> using <br/>" +
                    $"&emsp; Engine: <b> {_enginesUsed} </b> <br/>" +
                    $"&emsp; Origins: <b> {_originsUsed} </b> <br/>" +
                    $"&emsp; Servers: <b> {_serversUsed} s</b>" +
                    $"<br/>" +
                    $"<button type=\"submit\" id=\"btnUndo\" class=\"btn btn-info\">Undo</button>" +
                    $"</div>");
            }
            return base.GetMessage();
        }
    }
}