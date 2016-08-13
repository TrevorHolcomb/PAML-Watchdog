using System;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Xunit;

namespace AdministrationPortal.Tests
{
    public class RulesTests
    {
        private const string RuleName = "QueueSizeTooBig";

        private const string RuleDescription =
            "This rule checks to see if the queue has too many elements enqueued in it.";

        private const string RuleEngine = "N/A";

        private const string Origin = "TestingOrigin";
        private const string Server = "TestingServer";
        private const string CreatedBy = "Selenium & Coypu";
        private const string Expression = "";
        private const string DefaultSeverity = "2";
        private const string AlertType = "Standard Alert Type";
        private const string MessageType = "QueueSize";
        private const string SupportCategory = "Standard Support Group";


        private bool DoesRuleExist(string rulename, Scope browser)
        {
            foreach (var row in browser.FindAllCss(".rule-name"))
            {
                if (row.Text == rulename)
                {
                    return true;
                }
            }

            return false;
        }

        [Fact]
        public void CreateRuleTest()
        {
            using (var browser = new BrowserSession(new SessionConfiguration
            {
                AppHost = "http://localhost/",
                Port = 61061,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                Browser = Browser.Chrome
            }))
            {
                browser.Visit("/Rules");

                if (DoesRuleExist(RuleName, browser))
                {
                    throw new Exception("Rule Already Exists");
                }

                browser.ClickLink("Create New");

                browser.FillIn("name").With(RuleName);
                browser.FillIn("description").With(RuleDescription);
                browser.Select(RuleEngine).From("engine");
                browser.FillIn("origin").With(Origin);
                browser.FillIn("server").With(Server);
                browser.FillIn("RuleCreator").With(CreatedBy);
                browser.FillIn("expression").With(Expression);
                browser.Select(DefaultSeverity).From("DefaultSeverity");
                browser.Select(AlertType).From("AlertTypeId");
                browser.Select(MessageType).From("MessageTypeName");
                browser.Select(SupportCategory).From("SupportCategoryId");

                browser.ClickButton("Create");
            }
        }
    }
}
