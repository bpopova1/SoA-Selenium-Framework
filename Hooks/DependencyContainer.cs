using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using SeleniumFramework.Models;
using SeleniumFramework.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFramework.Hooks
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWebDriver>(sp =>
            {
                new DriverManager().SetUpDriver(new ChromeConfig());

                var driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                return driver;
            });

            services.AddSingleton<SettingsModel>(sp =>
            {
                return ConfigurationManager.Instance.SettingsModel;
            });

            return services;
        }
    }
}
