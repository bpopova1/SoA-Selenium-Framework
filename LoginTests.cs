using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumFramework.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFramework
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void LoginWith_NonExistingUser_ShowsValidationMessage()
        {
            _driver.Navigate().GoToUrl("https://www.politerock-eccbcd9f.westeurope.azurecontainerapps.io/");

            var loginPage = new LoginPage(_driver);
            loginPage.LoginWith("borislav.vaptsarov@endava.com", "wrongpassword");

            loginPage.VerifyPasswordInputIsEmpty();

            string errorDialogText = _driver.FindElement(By.ClassName("alert")).Text;
            Assert.That(errorDialogText, Is.EqualTo("Invalid email or password"));
        }
    }
}