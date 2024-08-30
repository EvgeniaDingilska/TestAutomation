using OpenQA.Selenium;
using System;
using System.Linq;

namespace TestAutomation.POM
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver webDriver) : base(webDriver) { }

        public string CartPrice => _webDriver.FindElement(By.CssSelector(".cart-bucket .grid-item-price")).Text;

        public int GetQuantityValueOfDropdown(int index)
        {
            var dropDown = _webDriver
                .FindElements(By.CssSelector(".grid-item-quantity .quantity")).ToArray();

            if (index > dropDown.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index must be between 0 - {dropDown.Length}");
            }

            var optionsInDropdown = dropDown.ElementAt(index - 1).FindElements(By.TagName("option"));

            foreach (var option in optionsInDropdown)
            {
                if (option.GetAttribute("selected") != null)
                {
                    return int.Parse(option.GetAttribute("value"));
                }
            }

            return 0;
        }
    }
}
