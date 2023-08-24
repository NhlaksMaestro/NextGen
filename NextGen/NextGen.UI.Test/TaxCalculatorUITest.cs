using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebDriverTest
{
    public class TaxCalculatorTest
    {
        static void Main(string[] args)
        {
            // Replace with the path to your own EdgeDriver executable
            var options = new EdgeOptions();
            options.UseChromium = true;
            var driver = new EdgeDriver(@"C:\path\to\edgedriver", options);

            driver.Url = "http://localhost:5000/Tax/CalculateTax";

            // Find the form elements and interact with them
            var emailInput = driver.FindElement(By.Id("Email"));
            emailInput.SendKeys("test@example.com");

            var postalCodeSelect = driver.FindElement(By.Id("PostalCode"));
            var selectElement = new SelectElement(postalCodeSelect);
            selectElement.SelectByValue("12345");

            var earningPerMonthInput = driver.FindElement(By.Id("EarningPerMonth"));
            earningPerMonthInput.SendKeys("5000");

            var earningPerYearInput = driver.FindElement(By.Id("EarningPerYear"));
            earningPerYearInput.SendKeys("60000");

            // Submit the form
            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            // Verify the results
            // ...

            driver.Quit();
        }
    }
}

