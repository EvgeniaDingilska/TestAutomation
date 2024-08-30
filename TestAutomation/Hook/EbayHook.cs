using CalculatorSelenium.Specs.Drivers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestAutomation.POM;

namespace TestAutomation.Hook
{
    [Binding]
    public class EbayHook
    {

        [BeforeScenario("EbayShop")]
        public static void BeforeScenario(BrowserDriver browserDriver)
        {
            var basePage = new BasePage(browserDriver.Current);
        }
    }
}