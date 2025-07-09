using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.UITests.Utils;

namespace SeleniumTests.UITests.PageObjects
{
    internal class LoginPage : BasePage
    {
        #region Page Elements

        private readonly By usernameInput = By.Id("userName");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login");
        private readonly By profileUserName = By.Id("userName-value");
        private readonly By logoutButton = By.Id("submit");

        #endregion

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        #region Methods

        public void NavigateToLoginPage()
        {
            Logger.Info("Navigating to DemoQA login page");
            driver.Navigate().GoToUrl("https://demoqa.com/login");
        }

        public void EnterCredentials(string username, string password)
        {
            Logger.Info($"Entering credentials for user: {username}");
            
            TestHelpers.WaitForElementToBeVisible(usernameInput, 10);
            TestHelpers.GetElement(usernameInput).Clear();
            TestHelpers.GetElement(usernameInput).SendKeys(username);

            TestHelpers.GetElement(passwordInput).Clear();
            TestHelpers.GetElement(passwordInput).SendKeys(password);
        }

        public void ClickLogin()
        {
            Logger.Info("Clicking login button");
            TestHelpers.ClickElement(loginButton);
        }

        public bool IsProfilePageDisplayed()
        {
            Logger.Info("Checking if profile page is displayed");
            try
            {
                TestHelpers.WaitForElementToBeVisible(profileUserName, 10);
                return TestHelpers.GetElement(profileUserName).Displayed;
            }
            catch (Exception ex)
            {
                Logger.Error($"Profile page not displayed: {ex.Message}");
                return false;
            }
        }

        public string GetProfileUserName()
        {
            Logger.Info("Getting profile username");
            TestHelpers.WaitForElementToBeVisible(profileUserName, 10);
            return TestHelpers.GetElement(profileUserName).Text;
        }

        #endregion
    }
}
