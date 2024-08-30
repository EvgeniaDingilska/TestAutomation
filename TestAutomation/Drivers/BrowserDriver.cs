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
            chromeOptions.AddArgument("--no-default-browser-check"); // Disables the default browser check prompt

            // Optionally, you can add other arguments to improve performance or customize the browser
            chromeOptions.AddArgument("--disable-extensions"); // Disable extensions
            chromeOptions.AddArgument("--disable-popup-blocking"); // Disable pop-up blocking
            chromeOptions.AddArgument("--start-maximized"); // Start the browser maximized

            var chromeDriver = new ChromeDriver("C:\\Users\\dingi\\source\\repos\\TestAutomation\\TestAutomation\\ChromeDriver\\chromedriver.exe", chromeOptions);
            
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