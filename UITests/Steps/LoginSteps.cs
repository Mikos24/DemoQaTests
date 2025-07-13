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

        [When(@"On Login page: I enter user name '(.*)' and password '(.*)'")]
        public void WhenIEnterValidCredentials(string userName, string password)
        {
            loginPage.EnterCredentials(userName, password);
            loginPage.ClickLogin();
        }

        [Then(@"I should see my profile page")]
        public void ThenIShouldSeeMyProfilePage()
        {
            Assert.That(loginPage.IsProfilePageDisplayed(), Is.True, "Profile page should be displayed after successful login");

            string profileUserName = loginPage.GetProfileUserName();
            Assert.That(profileUserName, Is.Not.Empty, "Profile username should be displayed");
        }
    }
}
