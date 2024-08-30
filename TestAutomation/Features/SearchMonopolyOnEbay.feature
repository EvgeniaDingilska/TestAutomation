Feature: Search Monopoly on eBay

@ToDoApp
Scenario: Search Monopoly on eBay
    Given I navigate to the eBay website on the following environment:
        | Browser | BrowserVersion | OS         |
        | Chrome  | 128.0          | Windows 11 |
    When I select Toys & Games from the drop-down list and search for Monopoly
    And I verify the first item has the title Monopoly
    And I verify that the item ships to Bulgaria
    Then I verify the item has a price displayed
    When I click on the first item
    Then I verify the item title contains Monopoly
    And I verify the price matches the search results page
    When I switch to the Shipping and payments view
    And I verify Bulgaria is in the country drop-down list
    When I select quantity 2 and add the item to the cart
    And I verify the cart URL is correct
    When I verify the quantity in the cart is 2
    And I verify the price is displayed for 2 items
