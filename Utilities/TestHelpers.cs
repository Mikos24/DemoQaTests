using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

internal static class TestHelpers
{
	private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	public static IWebDriver? Driver { get; set; }

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
	internal static void WaitForElementToBeVisible(By locator, int timeOutSeconds, string errorMessage = "Element was not found or was not visible.")
	{
		Log(NLog.LogLevel.Info, $"Waiting for element with locator: '{locator}' for up to '{timeOutSeconds}' seconds.");

		WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
		wait.Message = errorMessage;
		wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
	}

	/// <summary>
	/// Finds an element on the page.
	/// </summary>
	/// <param name="locator">The locator for the element.</param>
	/// <returns>The found IWebElement.</returns>
	internal static IWebElement GetElement(By locator)
	{
		Log(NLog.LogLevel.Info, $"Finding element with locator: '{locator}'");
		return Driver!.FindElement(locator);
	}

	/// <summary>
	/// Waits for an element to be present in the DOM and returns it.
	/// </summary>
	/// <param name="locator">The locator for the element.</param>
	/// <param name="timeOutSeconds">The maximum time to wait for the element.</param>
	/// <param name="errorMessage">The error message to display if the element is not found within the specified timeout.</param>
	/// <returns>The found IWebElement.</returns>
	/// <exception cref="WebDriverTimeoutException">Thrown when the element is not found within the specified timeout.</exception>
	internal static IWebElement WaitAndGetElement(By locator, int timeOutSeconds, string errorMessage = "Failed to get element in allocated time.")
	{
		Log(NLog.LogLevel.Info, $"Waiting and getting element with locator: '{locator}' for up to '{timeOutSeconds}' seconds.");

		WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
		wait.Message = errorMessage;
		wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

		return GetElement(locator);
	}

	/// <summary>
	/// Clicks an element found by the specified locator.
	/// </summary>
	/// <param name="locator">The locator for the element.</param>
	internal static void ClickElement(By locator)
	{
		Log(NLog.LogLevel.Info, $"Clicking element with locator: '{locator}'");
		GetElement(locator).Click();
	}

	/// <summary>
	/// Clicks the specified element.
	/// </summary>
	/// <param name="element">The element to click.</param>
	internal static void ClickElement(IWebElement element)
	{
		Log(NLog.LogLevel.Info, $"Clicking element: '{element}'");
		element.Click();
	}

	/// <summary>
	/// Finds multiple elements on the page.
	/// </summary>
	/// <param name="locator">The locator for the elements.</param>
	/// <returns>A list of found IWebElements.</returns>
	internal static IReadOnlyCollection<IWebElement> GetElements(By locator)
	{
		Log(NLog.LogLevel.Info, $"Finding elements with locator: '{locator}'");
		return Driver!.FindElements(locator);
	}

	/// <summary>
	/// Scrolls the specified element into view using JavaScript.
	/// </summary>
	/// <param name="element">The element to scroll into view.</param>
	internal static void ScrollElementIntoView(IWebElement element)
	{
		Log(NLog.LogLevel.Info, $"Scrolling element into view: '{element}'");

		// Scroll element into view using JavaScript and check if element is visible.
		// Repeat process until element is visible.
		for (int i = 0; i < 5; i++)
		{
			Log(NLog.LogLevel.Info, $"Attempt {i + 1} to scroll element into view: '{element}'");

			if (element.Displayed)
				break;

			((IJavaScriptExecutor)Driver!).ExecuteScript("arguments[0].scrollIntoView(true);", element);
			Thread.Sleep(500); // Wait for the element to scroll into view
		}
	}

	/// <summary>
	/// Checks if the specified element is displayed on the page.
	/// </summary>
	/// <param name="locator">The locator for the element to check.</param>
	/// <returns>True if the element is displayed; otherwise, false.</returns>
	internal static bool IsElementDisplayed(By locator)
	{
		Log(NLog.LogLevel.Info, $"Checking if element is displayed: '{locator}'");

		try
		{
			return Driver!.FindElement(locator).Displayed;
		}
		catch (NoSuchElementException)
		{
			return false;
		}
	}

	/// <summary>
	/// Waits for the specified element to become stale within the given time span.
	/// </summary>
	/// <param name="element">The web element to wait for.</param>
	/// <param name="timeOutSeconds">The number of seconds to wait before timing out.</param>
	internal static void WaitForElementToBecomeStale(IWebElement element, int timeOutSeconds)
	{
		Log(NLog.LogLevel.Info, $"Waiting for element to become stale: '{element}' for '{timeOutSeconds}' seconds.");

		TimeSpan timeOut = TimeSpan.FromSeconds(timeOutSeconds);
		WebDriverWait wait = new WebDriverWait(Driver!, timeOut);
		wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
	}

	/// <summary>
	/// Waits for the specified element to disappear from the page.
	/// </summary>
	/// <param name="locator">The locator for the element to wait for.</param>
	/// <param name="timeOutSeconds">The maximum time to wait for the element to disappear.</param>
	/// <param name="errorMessage">The error message to display if the element does not disappear within the specified timeout.</param>
	/// <exception cref="WebDriverTimeoutException">Thrown when the element does not disappear within the specified timeout.</exception>
	internal static void WaitForElementToDisappear(By locator, int timeOutSeconds, string errorMessage)
	{
		Log(NLog.LogLevel.Info, $"Waiting for element to disappear: '{locator}' for '{timeOutSeconds}' seconds.");

		WebDriverWait wait = new WebDriverWait(Driver!, TimeSpan.FromSeconds(timeOutSeconds));
		wait.Message = errorMessage;
		wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
	}
}

