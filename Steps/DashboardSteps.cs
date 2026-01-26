using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;

        public DashboardSteps(IWebDriver webDriver, SettingsModel model)
        {
            this._driver = webDriver;
            this._settingsModel = model;
        }

        [Then("I should see the logged user in the main header")]
        public void ThenIShouldSeeTheLoggedUserInTheMainHeader()
        {
            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyLoggedUserEmailIs(_settingsModel.Email);
            dashboardPage.VerifyUsernameIs(_settingsModel.Username);
        }

        [Then("I should be able to logout successfully")]
        public void ThenIShouldBeAbleToLogoutSuccessfully()
        {
            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.Logout();
        }
    }
}
