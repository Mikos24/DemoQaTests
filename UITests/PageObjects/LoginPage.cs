using DemoQATests.UITests.Utils;

namespace DemoQATests.UITests.PageObjects
{
    internal class LoginPage : BasePage
    {
        #region Locators
        private readonly Locator usernameInput = Locator.ById("userName");
        private readonly Locator passwordInput = Locator.ById("password");
        private readonly Locator loginButton = Locator.ById("login");
        private readonly Locator profileUserName = Locator.ById("userName-value");
        #endregion

        #region Constants
        private const string LOGIN_URL = "https://demoqa.com/login";
        private const int DEFAULT_WAIT_TIMEOUT = 10;
        #endregion

        public LoginPage() : base()
        {
        }

        public void NavigateToLoginPage()
        {
            Logger.Info("Navigating to DemoQA login page");
            TestHelpers.NavigateToUrl(LOGIN_URL);
        }

        public void EnterCredentials(string username, string password)
        {
            Logger.Info($"Entering credentials for user: {username}");
            
            TestHelpers.WaitForElementToBeVisible(usernameInput, DEFAULT_WAIT_TIMEOUT);
            TestHelpers.EnterText(usernameInput, username);
            TestHelpers.EnterText(passwordInput, password);
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
                TestHelpers.WaitForElementToBeVisible(profileUserName, DEFAULT_WAIT_TIMEOUT);
                return TestHelpers.IsElementDisplayed(profileUserName);
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
            return TestHelpers.WaitAndGetElementText(profileUserName, DEFAULT_WAIT_TIMEOUT);
        }
    }
}
