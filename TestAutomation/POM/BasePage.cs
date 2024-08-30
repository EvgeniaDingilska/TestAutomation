using Dynamitey;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TestAutomation.POM
{
    public class BasePage
    {
        private const string BasePageUrl = "https://www.ebay.com/";

        private readonly IWebDriver _webDriver;

        public const int DefaultWaitInSeconds = 5;

        public BasePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement CategoryDropDown => _webDriver.FindElement(By.CssSelector("#gh-cat-box"));
        public IWebElement[] CategoryDropDownOptions => _webDriver.FindElements(By.TagName("option")).ToArray();
        public IWebElement SearchInputField => _webDriver.FindElement(By.CssSelector("#gh-ac"));
        public IWebElement SearchButton => _webDriver.FindElement(By.CssSelector("#gh-btn"));

        //search results
        public IWebElement FirstItem => _webDriver.FindElements(By.CssSelector("#srp-river-results > ul >li")).First();
        public IWebElement FirstItemText => _webDriver.FindElement(By.CssSelector("#item3e28abcaca > div > div.s-item__info.clearfix > a > div > span"));
        public IWebElement FirstItemPrice => FirstItem.FindElement(By.XPath("//*[@id=\"item3e28abcaca\"]/div/div[2]/div[4]/div[1]/div[1]/span"));
        public string ShippingButon => FirstItem.FindElement(By.XPath("//*[@id=\"mainContent\"]/div[1]/div/div[1]/div[2]/div/button")).Text;

        //single page result
        public string SinglePageTitle => _webDriver.FindElement(By.XPath("//*[@id=\"mainContent\"]/div[1]/div[1]/h1/span")).Text;
        public string SinglePagePrice => _webDriver.FindElement(By.CssSelector(".x-bin-price__content .x-price-primary")).Text;
        public IWebElement PriceInput => _webDriver.FindElement(By.CssSelector("#qtyTextBox"));
        public IWebElement AddToCardButton => _webDriver.FindElement(By.CssSelector("#atcBtn_btn_1"));


        //Cart page 
        public string GetOptionValue()
        {
            var dropDown = _webDriver
                .FindElement(By.XPath("//*[@id=\"dropdown-1824768230-156fca84-7cea-413c-915b-33f512bc91b9\"]"));
            var optionsInDropdown = dropDown.FindElements(By.TagName("option"));

            foreach (var option in optionsInDropdown)
            {
                if (option.GetAttribute("selected") != null)
                {
                    return option.GetAttribute("value"); // Return the value of the selected option
                }
            }

            return null; // Return null if no option is selected
        }





        public string CurrentUrl()
        {
            return _webDriver.Url;
        }

        public void SetPrice(string number)
        {
            PriceInput.SendKeys(Keys.Clear);
            PriceInput.SendKeys(number);
        }
        public void SearchWithFilterAndCategory(string searchInputValue, string categoryValue)
        {
            CategoryDropDown.Click();
            IWebElement el = CategoryDropDownOptions.FirstOrDefault(el => el.Text == categoryValue);
            el.Click();

            SearchInputField.SendKeys(searchInputValue);

            SearchButton.Click();

        }

        public void LoginToBasePage()
        {
            _webDriver.Navigate().GoToUrl(BasePageUrl);
            new Actions(_webDriver)
                .SendKeys(Keys.Enter);
        }

        public void SwitchToLastTab()
        {
            var windowHandles = _webDriver.WindowHandles;
            _webDriver.SwitchTo().Window(windowHandles.Last());
        }
    }
}
