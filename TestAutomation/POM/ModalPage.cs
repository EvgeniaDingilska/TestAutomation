using OpenQA.Selenium;
using System.Linq;

namespace TestAutomation.POM
{
    public class ModalPage : BasePage
    {
        public ModalPage(IWebDriver webDriver) : base(webDriver) { }

        public IWebElement ShippingReturnAndPayments => _webDriver.FindElement(By.CssSelector(".lightbox-dialog__window.lightbox-dialog__window--animate.keyboard-trap--active"));
        public IWebElement CloseShippingButton => ShippingReturnAndPayments.FindElement(By.CssSelector(".icon-btn.lightbox-dialog__close"));

        public void OpenShippingReturnAndPayments()
        {
            var seeDetailsLink = new ProductPage(_webDriver).SeeDetailsLink;
            seeDetailsLink.Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public void CloseShippingReturnAndPayments()
        {
            CloseShippingButton.Click();
        }

        public bool IsBulgariaSelected()
        {
        if (ShippingReturnAndPayments.Displayed)
            {
                var deliveryTo = ShippingReturnAndPayments.FindElement(By.CssSelector("#shCountry"));
        var options = deliveryTo.FindElements(By.TagName("option")).ToArray().Where(el => int.Parse(el.GetAttribute("value")) == 34);
                return true;
            }

            return false;
        }
    }
}
