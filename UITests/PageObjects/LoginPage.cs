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
        private readonly Locator errorMessageByText = Locator.ByXPath("//*[contains(text(), 'Invalid') or contains(text(), 'invalid')]");
        private readonly Locator invalidFormControl = Locator.ByXPath("//input[@class='mr-sm-2 is-invalid form-control']");
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

        public void WaitForLoginResponse(int timeoutSeconds = DEFAULT_WAIT_TIMEOUT)
        {
            Logger.Info($"Waiting for login process to complete (timeout: {timeoutSeconds}s)");
            
            var endTime = DateTime.Now.AddSeconds(timeoutSeconds);
            const int pollingIntervalMs = 500;
            
            while (DateTime.Now < endTime)
            {
                var errorDisplayed = IsErrorMessageDisplayed();
                var loginPageDisplayed = IsLoginPageDisplayed();
                
                // If we have an error message OR we're no longer on the login page, we have a response
                if (errorDisplayed || !loginPageDisplayed)
                {
                    Logger.Info("Login process completed");
                    return; // Response received
                }
                
                // Still waiting for response, sleep briefly before checking again
                Thread.Sleep(pollingIntervalMs);
            }
            
            Logger.Error($"Timeout waiting for login process to complete after {timeoutSeconds} seconds");
            throw new TimeoutException($"Timeout waiting for login response after {timeoutSeconds} seconds");
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

        public bool IsLoginPageDisplayed()
        {
            Logger.Info("Checking if login page is displayed");
            // Check if login button is visible, which indicates we're still on the login page
            return TestHelpers.IsElementDisplayed(loginButton);
        }

        public bool IsErrorMessageDisplayed()
        {
            Logger.Info("Checking if error message is displayed");

            // Check for various possible error message locations on DemoQA
            var possibleErrorLocators = new[]
            {
                errorMessageByText,
                invalidFormControl
            };
            
            foreach (var locator in possibleErrorLocators)
            {
                if (TestHelpers.IsElementDisplayed(locator))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
