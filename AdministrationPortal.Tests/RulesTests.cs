using System;
using System.Linq;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using WatchdogDatabaseAccessLayer.Models;
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

        [Fact]
        public void CreateRuleTest()
        {
            using (var db = new WatchdogDatabaseContainer())
            using (var browser = new BrowserSession(new SessionConfiguration
            {
                AppHost = "http://localhost/",
                Port = 61061,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                Browser = Browser.Chrome
            }))
            {
                db.DeleteAll();
                db.SupportCategories.Add(new SupportCategory {Name = SupportCategory});
                db.SaveChanges();

                browser.Visit("/Rules");

                browser.ClickLink("Create New");

                browser.FillIn("Name").With(RuleName);
                browser.FillIn("Description").With(RuleDescription);
                browser.Select(RuleEngine).From("Engine");
                browser.FillIn("Origin").With(Origin);
                browser.FillIn("Server").With(Server);
                browser.FillIn("RuleCreator").With(CreatedBy);
                browser.FillIn("Expression").With(Expression);
                browser.Select(DefaultSeverity).From("DefaultSeverity");
                browser.Select(AlertType).From("AlertTypeId");
                browser.Select(MessageType).From("MessageTypeName");
                browser.Select(SupportCategory).From("SupportCategoryId");

                browser.ClickButton("Create");
            }
        }
    }
}
