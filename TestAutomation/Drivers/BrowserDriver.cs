using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace CalculatorSelenium.Specs.Drivers
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-first-run");
            chromeOptions.AddArgument("--no-default-browser-check");
            chromeOptions.AddArgument("--disable-default-apps");
            chromeOptions.AddArgument("--disable-popup-blocking");
            chromeOptions.AddArgument("--disable-features=PreloadMediaEngagementData,AutofillServerCommunication");
            chromeOptions.AddArgument("--disable-sync");

            var chromeDriver = new ChromeDriver("C:\\Users\\dingi\\source\\repos\\TestAutomation\\TestAutomation\\ChromeDriver\\chromedriver.exe", chromeOptions);
            chromeDriver.Manage().Window.Maximize();
            return chromeDriver;
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}