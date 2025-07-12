using NLog;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using DemoQATests.UITests.Utils;

namespace DemoQATests.UITests.Steps
{
    [Binding]
    public class ScenarioHooks
    {

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
