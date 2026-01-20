using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        // Elements 
        private IWebElement _emailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement _passwordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement _submitButton => _driver.FindElement(By.XPath("//button[@type='submit' and contains(text(), 'Sign In')]"));

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Actions
        public void LoginWith(string email, string password)
        {
            _emailInput.SendKeys(email);
            _passwordInput.SendKeys(password);

            _submitButton.Click();
        }

        // Validations
        public void VerifyPasswordInputIsEmpty()
        {
            string? text = _passwordInput.GetAttribute("value");

            Assert.That(text, Is.EqualTo(string.Empty));
        }

        public bool IsPasswordInputEmpty()
        {
            return string.IsNullOrWhiteSpace(_passwordInput.GetAttribute("value"));
        }
    }
}
