using NUnit.Framework;
using DemoQATests.UITests.PageObjects;
using DemoQATests.UITests.Utils;
using TechTalk.SpecFlow;

namespace DemoQATests.UITests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage loginPage;

        public LoginSteps()
        {
            loginPage = new LoginPage();
        }

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            loginPage.NavigateToLoginPage();
        }

        [When(@"On Login page: I will try to login with user name '(.*)' and password '(.*)'")]
        public void WhenIWillTryToLoginWithUserNameAndPassword(string userName, string password)
        {
            loginPage.EnterCredentials(userName, password);
            loginPage.ClickLogin();
            
            // Wait until we get a response from the login attempt
            loginPage.WaitForLoginResponse();
        }

        [Then(@"On Login page: I am successfully logged in = '(true|false)'")]
        public void ThenOnLoginPageIAmSuccessfullyLoggedIn(bool expectedLoggedIn)
        {
            if (expectedLoggedIn)
            {
                // For successful login: verify no errors and login page is no longer displayed
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.False, "No error message should be displayed for successful login");
                Assert.That(loginPage.IsLoginPageDisplayed(), Is.False, "Login page should no longer be displayed after successful login");
                
                // Optionally verify profile page is displayed
                Assert.That(loginPage.IsProfilePageDisplayed(), Is.True, "Profile page should be displayed after successful login");
            }
            else
            {
                // For failed login: verify errors are shown and still on login page
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, "Error message should be displayed for failed login");
                Assert.That(loginPage.IsLoginPageDisplayed(), Is.True, "Should still be on login page after failed login");
            }
        }
    }
}
