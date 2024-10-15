using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace JRoeSelenium.Helpers
{

    /// <summary>
    /// Class to help with Driver / Selenium type tasks can go here.
    /// </summary>
    public static class DriverHelper
    {
        /// <summary>
        /// Returns the name of the driver which the test is being run.
        /// </summary>
        /// <param name="driver"><typeparamref name="IWebDriver"/>WebDriver</param>
        /// <returns>Driver Name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetDriverName(IWebDriver driver)
        {
            string browserName = string.Empty;

            if (driver == null) throw new ArgumentNullException("Driver is null. Please set driver");

            if (driver.GetType() == typeof(ChromeDriver))
            {
                browserName = "Chrome";
            }
            else if (driver.GetType() == typeof(FirefoxDriver))
            {
                browserName = "Firefox";
            }
            else
            {
                TestContext.Out.WriteLine("Browser not recognized. Add driver to helper and try again.");
            }
            return browserName;
        }

    }
}
