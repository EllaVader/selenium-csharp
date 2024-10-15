using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace JRoeSelenium.Helpers
{
    /// <summary>
    /// This class has common waits that are used when
    /// state is changing on the elemnt.  This defines the Explicit wait for element
    /// that is often used when iteracting with elements.
    /// </summary>
    public static class WaitUtil
    {
        private WebDriverWait _wait;
        private IWebDriver _driver;
        private int _timeoutInSeconds;

        public WaitUtil(IWebDriver driver, int timeOutInSeconds = 3)
        {
            _timeoutInSeconds = timeOutInSeconds;
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOutInSeconds));
        }

        /// <summary>
        /// Wait for the element to become visible
        /// </summary>
        /// <param name="elementLocator"><typeparamref name="By"/></param>
        /// <returns><typeparamref name="IWebElement"/> found element</returns>
        public IWebElement WaitForElementVisible(By elementLocator)
        {
            TestContext.Out.WriteLine($"Waiting for element visible: {elementLocator.Criteria}");
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            return element;
        }

        /// <summary>
        /// Wait for the element to no onger exist or to no longer be visible on the DOM. 
        /// </summary>
        /// <param name="elementLocator"><typeparamref name="By"/></param>
        public void WaitForElementGone(By elementLocator)
        {
            TestContext.Out.WriteLine($"Waiting for element not visible: {elementLocator.Criteria}");
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
        }
    }
}
