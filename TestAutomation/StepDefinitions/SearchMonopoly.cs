using CalculatorSelenium.Specs.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TestAutomation.POM;

namespace TestAutomation.StepDefinitions
{
    [Binding]
    public sealed class SearchMonopoly
    {
        private readonly SearchPage _searchPage;
        private readonly SearchResultsPage _searchResultsPage;
        private readonly ProductPage _productPage;
        private readonly ModalPage _modalPage;
        private readonly CartPage _cartPage;

        private string priceFilePathOne = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "priceOne.txt");
        private string priceFilePathTwo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "priceTwo.txt");
        private string priceFilePathSumForSpecificQuantity = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "priceSum.txt");

        public SearchMonopoly(BrowserDriver browserDriver)
        {
            var webDriver = browserDriver.Current;
            _searchPage = new SearchPage(webDriver);
            _searchResultsPage = new SearchResultsPage(webDriver);
            _productPage = new ProductPage(webDriver);
            _modalPage = new ModalPage(webDriver);
            _cartPage = new CartPage(webDriver);
        }

        [Given(@"I navigate to the eBay website on the following environment:")]
        public void GivenINavigateToTheEBayWebsiteOnTheFollowingEnvironment(Table table)
        {
            _searchPage.LoginToBasePage();
        }

        [When(@"I select Toys & Games from the drop-down list and search for Monopoly")]
        public void WhenISelectToysGamesFromTheDrop_DownListAndSearchForMonopoly()
        {
            _searchPage.SearchWithFilterAndCategory("Monopoly", "Toys & Hobbies");
        }

        [When(@"I verify the first item has the title Monopoly")]
        public void WhenIVerifyTheFirstItemHasTheTitleMonopoly()
        {
            Assert.IsTrue(_searchResultsPage.FirstItemText.ToLower().Contains("monopoly"));
        }

        [When(@"I verify that the item ships to Bulgaria")]
        public void WhenIVerifyThatTheItemShipsToBulgaria()
        {
            Assert.IsTrue(_searchResultsPage.ShippingButton.Contains("Bulgaria"));
        }

        [Then(@"I verify the item has a price displayed")]
        public void ThenIVerifyTheItemHasAPriceDisplayed()
        {
            Assert.IsTrue(_searchResultsPage.FirstItemPrice.Displayed);
            File.WriteAllText(priceFilePathOne, Regex.Replace(_searchResultsPage.PriceValues().ElementAt(0), @"[^0-9.]", ""));
            File.WriteAllText(priceFilePathTwo, Regex.Replace(_searchResultsPage.PriceValues().ElementAt(1), @"[^0-9.]", ""));
        }

        [When(@"I click on the first item")]
        public void WhenIClickOnTheFirstItem()
        {
            _searchResultsPage.OpenFirstItem();
        }

        [Then(@"I verify the item title contains Monopoly")]
        public void ThenIVerifyTheItemTitleContainsMonopoly()
        {
            _productPage.SwitchToLastTab();
            Assert.IsTrue(_productPage.SinglePageTitle.Contains("monopoly"));
        }

        [Then(@"I verify the price matches the search results page")]
        public void ThenIVerifyThePriceMatchesTheSearchResultsPage()
        {
            double savedPriceOne = double.Parse(File.ReadAllText(priceFilePathOne));
            double savedPriceTwo = double.Parse(File.ReadAllText(priceFilePathTwo));
            var singleArticlePrice = Regex.Replace(_productPage.SinglePagePrice, @"[^0-9.]", "");
            Assert.That(double.Parse(singleArticlePrice), Is.InRange(savedPriceOne, savedPriceTwo));
        }

        [When(@"I switch to the Shipping and payments view")]
        public void WhenISwitchToTheShippingAndPaymentsView()
        {
            _productPage.OpenShipptingReturnAndPayments();
        }

        [When(@"I verify Bulgaria is in the country drop-down list")]
        public void WhenIVerifyBulgariaIsInTheCountryDrop_DownList()
        {
            Assert.True(_modalPage.IsBulgariaSelected());
            _modalPage.CloseShippingReturnAndPayments();
        }

        [When(@"I select quantity (.*) and add the item to the cart")]
        public void WhenISelectQuantityAndAddTheItemToTheCart(int quantity)
        {
            _productPage.ChooseTheProductOption(quantity);
            double singleArticlePrice = double.Parse(Regex.Replace(_productPage.SinglePagePrice, @"[^0-9.]", ""));
            var sumForSpecificQuantity = singleArticlePrice * quantity;
            File.WriteAllText(priceFilePathSumForSpecificQuantity, sumForSpecificQuantity.ToString());
            _productPage.SetQuantityAndProduct(quantity.ToString());
            _productPage.AddToCartButton();
        }

        [When(@"I verify the cart URL is correct")]
        public void WhenIVerifyTheCartURLIsCorrect()
        {
            Assert.AreEqual("https://cart.payments.ebay.com/", _cartPage.CurrentUrl());
        }

        [When(@"I verify the quantity in the cart is (.*)")]
        public void WhenIVerifyTheQuantityInTheCartIs(int quantity)
        {
            Assert.AreEqual(_cartPage.GetQuantityValueOfDropdown(1), quantity);
        }

        [When(@"I verify the price is displayed for (.*) items")]
        public void WhenIVerifyThePriceIsDisplayedForItems(int quantity)
        {
            string savedPriceSum = File.ReadAllText(priceFilePathSumForSpecificQuantity);
            Assert.AreEqual(double.Parse(Regex.Replace(_cartPage.CartPrice, @"[^0-9.]", "")), double.Parse(savedPriceSum));
        }
    }
}
