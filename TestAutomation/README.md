# Test Automation Project

This project is a Selenium-based test automation suite designed to automate browser interactions and verify functionalities on the eBay website. The project is structured using Page Object Model (POM) and utilizes NUnit for test management and assertions.

## Table of Contents

- [Project Overview](#project-overview)
- [Setup Instructions](#setup-instructions)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Handling Search Engine Choice Modal](#handling-search-engine-choice-modal)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

This project automates various interactions with the eBay website, such as searching for products, verifying product details, handling cart operations, and more. The test scenarios are written using C# and NUnit, leveraging Selenium WebDriver for browser automation.

## Setup Instructions

### Prerequisites

- .NET SDK (version 6.0 or later)
- Selenium (Latest version)
- Nunit (latest version)
- SpecFlow (latest version)
- Chrome browser (latest version)
- ChromeDriver (compatible with your Chrome browser version)

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/EvgeniaDingilska/TestAutomation.git
    ```

2. **Install dependencies:**

    Run the following command to restore NuGet packages:

    ```bash
    dotnet restore
    ```

3. **Update WebDriver Path:**

   Ensure that the `chromedriver.exe` is included in your project or available in your system's PATH.

## Running Tests

### Using .NET CLI

To run the tests, navigate to the project directory and use the following command:

```bash
dotnet test

Project Structure
The project is structured using the Page Object Model (POM) design pattern. Below is an overview of the key components:

Drivers

BrowserDriver.cs: Manages the browser driver initialization and handles interactions like dismissing pop-ups or modals.
POM (Page Object Model)

BasePage.cs: Contains common methods shared across different pages.
SearchPage.cs: Handles actions related to the search functionality.
SearchResultsPage.cs: Manages the interactions on the search results page.
ProductPage.cs: Contains methods to interact with the product details page.
ModalPage.cs: Handles modal dialogs, such as the search engine choice modal.
CartPage.cs: Manages the cart-related operations.
Step Definitions

SearchMonopoly.cs: Contains the step definitions for the Monopoly search test scenario.
Handling Search Engine Choice Modal
On Chrome, a search engine choice modal (search-engine-choice-app) may appear due to regional laws. This modal is automatically dismissed by the BrowserDriver class using the following logic:

The browser waits for the modal to appear.
The "Close" button on the modal is located and clicked to dismiss it.
This ensures the test flow is not interrupted by the modal.

Contributing
Contributions are welcome! Please follow these steps to contribute:

Fork the repository.
Create a new branch (git checkout -b feature-branch).
Make your changes and commit them (git commit -m 'Add some feature').
Push to the branch (git push origin feature-branch).
Open a pull request.
Please ensure your code follows the project's coding standards and includes appropriate test coverage.