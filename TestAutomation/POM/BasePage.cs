using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestAutomation.POM
{
    public class BasePage
    {
        protected readonly IWebDriver _webDriver;

        public const int DefaultWaitInSeconds = 5;
        private const string BasePageUrl = "https://www.ebay.com/";

        public BasePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void LoginToBasePage()
        {
            _webDriver.Navigate().GoToUrl(BasePageUrl);
            new Actions(_webDriver)
                .SendKeys(Keys.Enter);
        }

        public string CurrentUrl()
        {
            return _webDriver.Url;
        }

        public void SwitchToLastTab()
        {
            var windowHandles = _webDriver.WindowHandles;
            _webDriver.SwitchTo().Window(windowHandles.Last());
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }
    }
}
