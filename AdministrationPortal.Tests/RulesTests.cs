using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using Xunit;

namespace AdministrationPortal.Tests
{
    public class RulesTests
    {
        [Fact]
        public void CreateRuleTest()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
        }
    }
}
