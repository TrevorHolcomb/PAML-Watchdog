using System.Collections.Generic;
using System.Linq;
using Watchdog.RuleEngine;
using WatchdogDaemon.RuleEngine;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogDaemon.Tests
{

    public class RuleEngineTests
    {
        public static TheoryData<Rule, Message> ShouldGenerateAlertData = new TheoryData<Rule, Message>
        {

        };

        public static TheoryData<Rule, Message> ShouldntGenerateAlertData = new TheoryData<Rule, Message>
        {

        };


        [Theory]
        [MemberData(nameof(ShouldGenerateAlertData))]
        public void ShouldGenerateAlertTest(Rule rule, Message message)
        {
            var ruleEngine = new StandardRuleEngine();
            Assert.NotNull(ruleEngine.ConsumeMessage(rule, message));
        }

        [Theory]
        [MemberData(nameof(ShouldntGenerateAlertData))]
        public void ShouldntGenerateAlertTest(Rule rule, Message message)
        {
            var ruleEngine = new StandardRuleEngine();
            Assert.Null(ruleEngine.ConsumeMessage(rule, message));
        }
    }
}