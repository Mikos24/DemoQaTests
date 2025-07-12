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

        [When(@"I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            loginPage.EnterCredentials("testuser", "Password123!");
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
