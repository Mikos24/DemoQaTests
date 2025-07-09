using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DemoQATests.UITests.Utils
{
    public class WebDriverFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IWebDriver CreateDriver(string browserType = "chrome")
        {
            Logger.Info($"Creating WebDriver instance for browser: {browserType}");

            IWebDriver driver;

            switch (browserType.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                default:
                    throw new ArgumentException($"Browser type '{browserType}' is not supported");
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Logger.Info("WebDriver instance created successfully");
            return driver;
        }
    }
}
