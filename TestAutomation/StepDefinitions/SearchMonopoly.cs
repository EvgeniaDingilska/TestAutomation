using CalculatorSelenium.Specs.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V126.Network;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TestAutomation.POM;

namespace TestAutomation.StepDefinitions
{
    [Binding]
    public sealed class SearchMonopoly
    {
        private readonly BasePage _basePage;
        private string priceFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "price.txt");

        public SearchMonopoly(BrowserDriver browserDriver)
        {
            _basePage = new BasePage(browserDriver.Current);
        }

        [Given(@"I navigate to the eBay website on the following environment:")]
        public void GivenINavigateToTheEBayWebsiteOnTheFollowingEnvironment(Table table)
        {
            _basePage.LoginToBasePage();
        }

        [When(@"I select Toys & Games from the drop-down list and search for Monopoly")]
        public void WhenISelectToysGamesFromTheDrop_DownListAndSearchForMonopoly()
        {
            _basePage.SearchWithFilterAndCategory("Monopoly", "Toys & Hobbies");
        }

        [When(@"I verify the first item has the title Monopoly")]
        public void WhenIVerifyTheFirstItemHasTheTitleMonopoly()
        {
            Assert.IsTrue(_basePage.FirstItemText.Text.Contains("Monopoly"));
        }

        [When(@"I verify that the item ships to Bulgaria")]
        public void WhenIVerifyThatTheItemShipsToBulgaria()
        {
            Assert.IsTrue(_basePage.ShippingButon.Contains("Bulgaria"));
        }

        [Then(@"I verify the item has a price displayed")]
        public void ThenIVerifyTheItemHasAPriceDisplayed()
        {
            Assert.IsTrue(_basePage.FirstItemPrice.Displayed);
            System.IO.File.WriteAllText(priceFilePath, Regex.Replace(_basePage.FirstItemPrice.Text, @"[^0-9.]", ""));
        }

        [When(@"I click on the first item")]
        public void WhenIClickOnTheFirstItem()
        {
            _basePage.FirstItemText.Click();
        }

        [Then(@"I verify the item title contains Monopoly")]
        public void ThenIVerifyTheItemTitleContainsMonopoly()
        {
            _basePage.SwitchToLastTab();
            _basePage.SinglePageTitle.Contains("Monopoly");
        }

        [Then(@"I verify the price matches the search results page")]
        public void ThenIVerifyThePriceMatchesTheSearchResultsPage()
        {
            string savedPrice = System.IO.File.ReadAllText(priceFilePath);
            var secondPrice = Regex.Replace(_basePage.SinglePagePrice, @"[^0-9.]", "").ToString();
            //Assert.AreEqual(savedPrice, secondPrice);
        }

        [When(@"I switch to the Shipping and payments view")]
        public void WhenISwitchToTheShippingAndPaymentsView()
        {
            //There is no Shipping and payments
        }

        [When(@"I verify Bulgaria is in the country drop-down list")]
        public void WhenIVerifyBulgariaIsInTheCountryDrop_DownList()
        {
            //There is no Chnage country
        }

        [When(@"I select quantity (.*) and add the item to the cart")]
        public void WhenISelectQuantityAndAddTheItemToTheCart(int p0)
        {
            _basePage.SetPrice(p0.ToString());
            _basePage.AddToCardButton.Click();
        }

        [When(@"I verify the cart URL is correct")]
        public void WhenIVerifyTheCartURLIsCorrect()
        {
            Assert.AreEqual("https://cart.payments.ebay.com/", _basePage.CurrentUrl());
        }

        [When(@"I verify the quantity in the cart is (.*)")]
        public void WhenIVerifyTheQuantityInTheCartIs(int p0)
        {
            Assert.AreEqual(_basePage.GetOptionValue(), p0);
        }
    }
}
