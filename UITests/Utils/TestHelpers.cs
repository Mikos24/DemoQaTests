using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoQATests.UITests.Utils
{
    internal static class TestHelpers
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static IWebDriver? Driver { get; set; }

        //Navigates to provided URL
        internal static void NavigateToUrl(string url)
        {
            Log(NLog.LogLevel.Info, $"Navigating to URL: {url}");
            Driver!.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Logs a message with the specified log level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The message to log.</param>
        internal static void Log(NLog.LogLevel level, string message)
        {
            Logger.Log(level, message);
        }

        /// <summary>
        /// Waits for an element to be visible on the page.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        /// <param name="timeOutSeconds">The maximum time to wait for the element.</param>
        /// <param name="errorMessage">The error message to display if the element is not visible within the specified timeout.</param>
        /// <exception cref="WebDriverTimeoutException">Thrown when the element is not found within the specified timeout.</exception>
        internal static void WaitForElementToBeVisible(Locator locator, int timeOutSeconds, string errorMessage = "Element was not found or was not visible.")
        {
            Log(NLog.LogLevel.Info, $"Waiting for element with locator: '{locator}' for up to '{timeOutSeconds}' seconds.");

            WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
            wait.Message = errorMessage;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator.ToSeleniumBy()));
        }

        /// <summary>
        /// Finds an element on the page.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        /// <returns>The found IWebElement.</returns>
        internal static IWebElement GetElement(Locator locator)
        {
            Log(NLog.LogLevel.Info, $"Finding element with locator: '{locator}'");
            return Driver!.FindElement(locator.ToSeleniumBy());
        }

        /// <summary>
        /// Waits for an element to be present in the DOM and returns it.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        /// <param name="timeOutSeconds">The maximum time to wait for the element.</param>
        /// <param name="errorMessage">The error message to display if the element is not found within the specified timeout.</param>
        /// <returns>The found IWebElement.</returns>
        /// <exception cref="WebDriverTimeoutException">Thrown when the element is not found within the specified timeout.</exception>
        internal static IWebElement WaitAndGetElement(Locator locator, int timeOutSeconds, string errorMessage = "Failed to get element in allocated time.")
        {
            Log(NLog.LogLevel.Info, $"Waiting and getting element with locator: '{locator}' for up to '{timeOutSeconds}' seconds.");

            WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
            wait.Message = errorMessage;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator.ToSeleniumBy()));

            return GetElement(locator);
        }

        /// <summary>
        /// Clicks an element found by the specified locator.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        internal static void ClickElement(Locator locator)
        {
            Log(NLog.LogLevel.Info, $"Clicking element with locator: '{locator}'");
            GetElement(locator).Click();
        }


        /// <summary>
        /// Finds multiple elements on the page.
        /// </summary>
        /// <param name="locator">The locator for the elements.</param>
        /// <returns>A list of found IWebElements.</returns>
        internal static IReadOnlyCollection<IWebElement> GetElements(Locator locator)
        {
            Log(NLog.LogLevel.Info, $"Finding elements with locator: '{locator}'");
            return Driver!.FindElements(locator.ToSeleniumBy());
        }


        /// <summary>
        /// Checks if the specified element is displayed on the page.
        /// </summary>
        /// <param name="locator">The locator for the element to check.</param>
        /// <returns>True if the element is displayed; otherwise, false.</returns>
        internal static bool IsElementDisplayed(Locator locator)
        {
            Log(NLog.LogLevel.Info, $"Checking if element is displayed: '{locator}'");

            try
            {
                return Driver!.FindElement(locator.ToSeleniumBy()).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits for the specified element to disappear from the page.
        /// </summary>
        /// <param name="locator">The locator for the element to wait for.</param>
        /// <param name="timeOutSeconds">The maximum time to wait for the element to disappear.</param>
        /// <param name="errorMessage">The error message to display if the element does not disappear within the specified timeout.</param>
        /// <exception cref="WebDriverTimeoutException">Thrown when the element does not disappear within the specified timeout.</exception>
        internal static void WaitForElementToDisappear(Locator locator, int timeOutSeconds, string errorMessage)
        {
            Log(NLog.LogLevel.Info, $"Waiting for element to disappear: '{locator}' for '{timeOutSeconds}' seconds.");

            WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
            wait.Message = errorMessage;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator.ToSeleniumBy()));
        }

        /// <summary>
        /// Enters text into an input field after clearing it.
        /// </summary>
        /// <param name="locator">The locator for the input element.</param>
        /// <param name="text">The text to enter.</param>
        internal static void EnterText(Locator locator, string text)
        {
            Log(NLog.LogLevel.Info, $"Entering text '{text}' into element with locator: '{locator}'");
            var element = GetElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Gets the text content of an element.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        /// <returns>The text content of the element.</returns>
        internal static string GetElementText(Locator locator)
        {
            Log(NLog.LogLevel.Info, $"Getting text from element with locator: '{locator}'");
            return GetElement(locator).Text;
        }

        /// <summary>
        /// Waits for an element to be visible and then gets its text.
        /// </summary>
        /// <param name="locator">The locator for the element.</param>
        /// <param name="timeOutSeconds">The maximum time to wait for the element.</param>
        /// <returns>The text content of the element.</returns>
        internal static string WaitAndGetElementText(Locator locator, int timeOutSeconds)
        {
            Log(NLog.LogLevel.Info, $"Waiting and getting text from element with locator: '{locator}' for up to '{timeOutSeconds}' seconds.");
            WaitForElementToBeVisible(locator, timeOutSeconds);
            return GetElementText(locator);
        }
    }
}
