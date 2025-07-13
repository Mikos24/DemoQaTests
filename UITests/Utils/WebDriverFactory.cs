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
        private static readonly Logger Logger = LoggingConfig.GetCurrentClassLogger();

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            Logger.Info("WebDriver instance created and configured successfully");
            return driver;
        }

        private static IWebDriver CreateChromeDriver(bool headless)
        {
            try
            {
                Logger.Debug("Setting up Chrome driver");
                new DriverManager().SetUpDriver(new ChromeConfig());
                
                var options = new ChromeOptions();
                
                // Configure headless mode with improved options
                if (headless)
                {
                    options.AddArguments(
                        "--headless=new",           // Use new headless mode (Chrome 109+)
                        "--no-sandbox",
                        "--disable-dev-shm-usage",
                        "--disable-gpu",            // Disable GPU in headless mode
                        "--window-size=1920,1080"   // Set consistent window size
                    );
                }

                // Add stability and performance options
                options.AddArguments(
                    "--disable-blink-features=AutomationControlled",  // Hide automation detection
                    "--disable-extensions",                            // Disable extensions for stability
                    "--no-first-run",                                  // Skip first run wizards
                    "--disable-default-apps",                          // Disable default apps
                    "--disable-infobars"                               // Disable info bars
                );

                // Enhanced preferences to disable popups and notifications
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);  // Block notifications
                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);                // Block popups

                // Hide automation indicators
                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalOption("useAutomationExtension", false);

                Logger.Debug("Chrome driver configured successfully");
                return new ChromeDriver(options);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to create Chrome driver");
                throw;
            }
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
