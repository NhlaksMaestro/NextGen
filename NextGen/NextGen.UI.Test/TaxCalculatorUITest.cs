using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebDriverTest
{
    [TestClass]
    public class TaxCalculatorTests
    {
        private EdgeDriver _driver;
        private string _url;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize Edge WebDriver
            _driver = new EdgeDriver();
            _url = "https://localhost:7137/Tax/Index";
        }

        [TestMethod]
        public void VerifyIndexPageTitle()
        {
            // Navigate to the index page of TaxController
            _driver.Url = _url;

            string expectedTitle = "Tax Calculator";

            Assert.AreEqual(expectedTitle, _driver.Title);
        }

        [TestMethod]
        public void VerifyEmailValidationErrorMessage()
        {
            _driver.Url = _url; 

            // Enter an invalid email
            _driver.FindElementByCssSelector("#Email").SendKeys("invalid-email");

            // Click a different field to trigger validation
            _driver.FindElementByCssSelector("#EarningPerMonth").Click();

            // Find the validation error message
            var errorMessage = _driver.FindElementByCssSelector("#Email-error");

            // Assert the error message text
            Assert.AreEqual("Invalid email format.", errorMessage.Text);
        }

        [TestMethod]
        public void VerifyEarningPerYearRequiredErrorMessage()
        {
            _driver.Url = _url;

            // Click the "Calculate" button without entering EarningPerYear
            _driver.FindElementByCssSelector("#calculateButton").Click();

            // Find the validation error message for EarningPerYear
            var errorMessage = _driver.FindElementByCssSelector("#EarningPerYear-error");

            // Assert the error message text
            Assert.AreEqual("The EarningPerYear field is required.", errorMessage.Text);
        }

        [TestMethod]
        public void VerifySuccessfulTaxCalculation()
        {
            _driver.Url = _url;

            // Enter valid data for tax calculation
            _driver.FindElementByCssSelector("#Email").SendKeys("valid-email@example.com");
            _driver.FindElementByCssSelector("#EarningPerYear").SendKeys("50000");
            _driver.FindElementByCssSelector("#PostalCodeId").SendKeys("7441");

            // Click the "Calculate" button
            _driver.FindElementByCssSelector("#calculateButton").Click();

            // Find the tax result element
            var taxResult = _driver.FindElementByCssSelector("#taxResult");

            // Assert that tax calculation result is displayed
            Assert.IsTrue(taxResult.Displayed);
        }

        [TestMethod]
        public void VerifyInvalidPostalCodeErrorMessage()
        {
            _driver.Url = _url;

            // Enter an invalid postal code
            _driver.FindElementByCssSelector("#PostalCodeId").SendKeys("123456");

            // Click the "Calculate" button
            _driver.FindElementByCssSelector("#calculateButton").Click();

            // Find the validation error message for PostalCodeId
            var errorMessage = _driver.FindElementByCssSelector("#PostalCodeId-error");

            // Assert the error message text
            Assert.AreEqual("The field Postal Code must match a valid postal code.", errorMessage.Text);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}

