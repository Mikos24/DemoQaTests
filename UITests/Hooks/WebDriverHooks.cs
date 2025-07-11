using NLog;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using DemoQATests.UITests.Utils;

namespace DemoQATests.UITests.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        [BeforeTestRun]
        public static void GlobalSetup()
        {
            // Only setup logging - WebDriverFactory handles driver setup
            LogManager.Setup().LoadConfigurationFromFile("NLog.config");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            TestHelpers.Driver = WebDriverFactory.CreateDriver("chrome");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            TestHelpers.Driver?.Quit();
        }
    }
}
