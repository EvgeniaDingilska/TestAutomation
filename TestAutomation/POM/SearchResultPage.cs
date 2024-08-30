using OpenQA.Selenium;
using System.Linq;

namespace TestAutomation.POM
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver webDriver) : base(webDriver) { }

        public IWebElement FirstItem => _webDriver.FindElements(By.CssSelector(".s-item.s-item__dsa-on-bottom.s-item__pl-on-bottom .s-item__wrapper.clearfix")).FirstOrDefault();
        public string FirstItemText => FirstItem.FindElement(By.CssSelector(".s-item__title")).Text;
        public IWebElement FirstItemPrice => FirstItem.FindElement(By.CssSelector(".s-item__price"));
        public string ShippingButton => FirstItem.FindElement(By.XPath("//*[@id=\"mainContent\"]/div[1]/div/div[1]/div[2]/div/button")).Text;

        public void OpenFirstItem()
        {
            FirstItem.Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public string[] PriceValues()
        {
            if (FirstItemPrice.Text.Contains("to"))
            {
                return FirstItemPrice.Text.Split(new string[] { "to" }, StringSplitOptions.None);
            }
            return new string[] { "0", FirstItemPrice.Text };
        }
    }
}
