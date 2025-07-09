using NUnit.Framework;
using OpenQA.Selenium;
using DemoQATests.UITests.PageObjects;
using TechTalk.SpecFlow;

namespace DemoQATests.UITests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginSteps(IWebDriver driver)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver);
        }

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.EnterCredentials("testuser", "Password123!");
            _loginPage.ClickLogin();
        }

        [Then(@"I should see my profile page")]
        public void ThenIShouldSeeMyProfilePage()
        {
            Assert.That(_loginPage.IsProfilePageDisplayed(), Is.True, "Profile page should be displayed after successful login");
            
            string profileUserName = _loginPage.GetProfileUserName();
            Assert.That(profileUserName, Is.Not.Empty, "Profile username should be displayed");
        }
    }
}
