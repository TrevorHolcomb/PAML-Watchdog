using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Xunit;

namespace AdministrationPortal.Tests
{
    public class RulesTests
    {
        [Fact]
        public void CreateRuleTest()
        {
            using (var browser = new BrowserSession(new SessionConfiguration
            {
                AppHost = "http://localhost/",
                Port = 61061,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                Browser = Browser.InternetExplorer
            }))
            {
                browser.Visit("/Rules");
            }
        }
    }
}
