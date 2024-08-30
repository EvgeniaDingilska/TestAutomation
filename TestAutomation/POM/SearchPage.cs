using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace TestAutomation.POM
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver webDriver) : base(webDriver) { }

        private IWebElement CategoryDropDown => _webDriver.FindElement(By.CssSelector("#gh-cat-box"));
        public IWebElement[] CategoryDropDownOptions => _webDriver.FindElements(By.TagName("option")).ToArray();
        public IWebElement SearchInputField => _webDriver.FindElement(By.CssSelector("#gh-ac"));
        public IWebElement SearchButton => _webDriver.FindElement(By.CssSelector("#gh-btn"));

        public void SearchWithFilterAndCategory(string searchInputValue, string categoryValue)
        {
            CategoryDropDown.Click();
            IWebElement el = CategoryDropDownOptions.FirstOrDefault(el => el.Text == categoryValue);
            el.Click();

            SearchInputField.SendKeys(searchInputValue);
            SearchButton.Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }
    }
}
