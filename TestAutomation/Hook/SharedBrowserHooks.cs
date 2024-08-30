using BoDi;
using CalculatorSelenium.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CalculatorSelenium.Specs.Hooks
{
    [Binding]
    public class SharedBrowserHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(ObjectContainer testThreadContainer)
        {
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        }
    }
}
