using BoDi;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using DemoQATests.UITests.Utils;

namespace DemoQATests.UITests.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver = null!;

        public WebDriverHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void GlobalSetup()
        {
            LogManager.Setup().LoadConfigurationFromFile("NLog.config");
            new DriverManager().SetUpDriver(new ChromeConfig());
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            TestHelpers.Driver = _driver;
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }
    }
}
