using NLog;
using OpenQA.Selenium;

namespace DemoQATests.UITests.PageObjects
{
	internal abstract class BasePage
	{
		protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		protected readonly IWebDriver driver;

		protected BasePage(IWebDriver driver)
		{
			this.driver = driver;
		}
	}
}
