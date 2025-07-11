using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DemoQATests.UITests.Utils
{
    public static class WebDriverFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Creates a WebDriver instance for the specified browser type
        /// </summary>
        /// <param name="browserType">Browser type: "chrome", "firefox", "edge"</param>
        /// <param name="headless">Run browser in headless mode</param>
        /// <returns>Configured WebDriver instance</returns>
        public static IWebDriver CreateDriver(string browserType = "chrome", bool headless = false)
        {
            Logger.Info($"Creating WebDriver instance for browser: {browserType}, headless: {headless}");

            IWebDriver driver = browserType.ToLower() switch
            {
                "chrome" => CreateChromeDriver(headless),
                "firefox" => CreateFirefoxDriver(headless),
                "edge" => CreateEdgeDriver(headless),
                _ => throw new ArgumentException($"Browser type '{browserType}' is not supported. Supported browsers: chrome, firefox, edge")
            };

            // Common driver configuration
            if (!headless)
            {
                driver.Manage().Window.Maximize();
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Logger.Info("WebDriver instance created and configured successfully");
            return driver;
        }

        private static IWebDriver CreateChromeDriver(bool headless)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            
            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }
            
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver(bool headless)
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var options = new FirefoxOptions();
            
            if (headless)
            {
                options.AddArgument("--headless");
            }
            
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver(bool headless)
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();
            
            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }
            
            return new EdgeDriver(options);
        }
    }
}
