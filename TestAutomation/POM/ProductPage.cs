using OpenQA.Selenium;

namespace TestAutomation.POM
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver webDriver) : base(webDriver) { }

        public string SinglePageTitle => _webDriver.FindElement(By.XPath("//*[@id=\"mainContent\"]/div[1]/div[1]/h1/span")).Text.ToLower();
        public string SinglePagePrice => _webDriver.FindElement(By.CssSelector(".x-bin-price__content .x-price-primary")).Text;
        public IWebElement QuantityInput => _webDriver.FindElement(By.CssSelector("#qtyTextBox"));
        public IWebElement SeeDetailsLink => _webDriver.FindElement(By.CssSelector(".ux-labels-values.col-12.ux-labels-values--shipping .ux-action.fake-link.fake-link--action"));
        public IWebElement DropDownOptionsButton => _webDriver.FindElement(By.CssSelector(".listbox-button__control.btn.btn--form.btn--truncated"));
        public IWebElement DropDownList => _webDriver.FindElement(By.CssSelector(".listbox__options.listbox-button__listbox"));

        public void AddToCartButton()
        {
            _webDriver.FindElement(By.CssSelector("#atcBtn_btn_1")).Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public void SetQuantityAndProduct(string number, int numberOfOption = 2)
        {
            if (!QuantityInput.Enabled)
            {
                ChooseTheProductOption(numberOfOption);
                QuantityInput.Clear();
                QuantityInput.SendKeys(number);
            }
            else
            {
                QuantityInput.Clear();
                QuantityInput.SendKeys(number);
            }
        }

        public void ChooseTheProductOption(int positiveNumber)
        {
            DropDownOptionsButton.Click();
            for (int i = 0; i < positiveNumber; i++)
            {
                DropDownList.SendKeys(Keys.Down);
            }
            DropDownList.SendKeys(Keys.Enter);
        }

        public void OpenShipptingReturnAndPayments()
        {
            SeeDetailsLink.Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
    }
}
