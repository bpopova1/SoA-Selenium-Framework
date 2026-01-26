using Bogus;
using OpenQA.Selenium;
using SeleniumFramework.Models;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities;

namespace SeleniumFramework
{
    [Ignore("No longer needed in this implementation")]
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;

        private readonly SettingsModel _settingsModel;

        public LoginTests()
        {
            this._settingsModel = ConfigurationManager.Instance.SettingsModel;
        }

        [SetUp]
        public void Setup()
        {
            
            _loginPage = new LoginPage(_driver);
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void LoginWith_ExistingUser_ShowsShowTheDashboard()
        {
            _loginPage.VerifyTheFormIsVisible();
            _loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);

            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyLoggedUserEmailIs(_settingsModel.Email);
            dashboardPage.VerifyUsernameIs(_settingsModel.Username);
        }

        [Test]
        public void LoginWith_NonExistingUser_ShowsValidationMessage()
        {
            var faker = new Faker();
            _loginPage.LoginWith(faker.Internet.Email(), faker.Internet.Password());

            Retry.Until(() =>
            {
                if (!_loginPage.IsPasswordEmpty())
                    throw new RetryException("Password input is not empty yet.");
            });

            _loginPage.VerifyPasswordInputIsEmpty();
            _loginPage.VerifyErrorMessageIsDisplayed("Invalid email or password");
        }
    }
}