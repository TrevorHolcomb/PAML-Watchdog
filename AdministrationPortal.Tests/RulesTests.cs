using System;
using System.Linq;
using System.Threading;
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
        private const string AlertTypeName = "Standard Alert Type";
        private const string AlertTypeDescription = "the standard alert type";
        private const string MessageTypeName = "QueueSize";

        private const string MessageTypeDescription =
            "A simple update from a queue with the number of elements enqueued in it";

        private const string SupportCategory = "Standard Support Group";
        private const string SupportCategoryDescription = "The standard support group";
        private const string RuleCategoryName = "N/A";

        private const string RuleCategoryDescription =
            "This is a rule category for rules that don't mach into a single group";

        private void BuildMessageType(WatchdogDatabaseContainer db)
        {
            var messageType = new MessageType
            {
                Name = RulesTests.MessageTypeName,
                Description = MessageTypeDescription
            };

            var messageTypeParameterType = new MessageTypeParameterType
            {
                Name = "QueueSize",
                MessageType = messageType,
                Required = true,
                Type = "integer"
            };

            messageType.MessageTypeParameterTypes.Add(messageTypeParameterType);

            db.MessageTypes.Add(messageType);
            db.MessageTypeParameterTypes.Add(messageTypeParameterType);
        }

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
                db.SupportCategories.Add(new SupportCategory {Name = SupportCategory, Description = SupportCategoryDescription});
                db.Engines.Add(new Engine {Name = RuleEngine});
                db.AlertTypes.Add(new AlertType {Name = AlertTypeName, Description =  AlertTypeDescription});
                db.RuleCategories.Add(new RuleCategory {Name = RuleCategoryName, Description = RuleCategoryDescription});
                BuildMessageType(db);


                db.SaveChanges();

                browser.Visit("/Rules");

                browser.ClickLink("Create New");

                browser.FillIn("Name").With(RuleName);
                browser.FillIn("Description").With(RuleDescription);
                browser.Select(RuleEngine).From("Engine");
                browser.FillIn("Origin").With(Origin);
                browser.FillIn("Server").With(Server);
                browser.FillIn("RuleCreator").With(CreatedBy);
                
                // Build Expression
                browser.Select("QueueSize").From("builder_rule_0_filter");
                browser.Select("greater").From("builder_rule_0_operator");
                browser.FillIn("builder_rule_0_value_0").With("10");

                browser.Select(DefaultSeverity).From("DefaultSeverity");
                browser.Select(AlertTypeName).From("AlertTypeId");
                browser.Select(MessageTypeName).From("MessageTypeName");
                browser.Select(SupportCategory).From("SupportCategoryId");

                // Select Rule Category
                browser.ExecuteScript($@"$('#SelectedRuleCategoryIds').multiselect('select', '{db.RuleCategories.Single(e => e.Name == RuleCategoryName).Id}')");
                
                browser.ClickButton("Create");
                Assert.Equal(1, db.Rules.Count());
            }
        }
    }
}
