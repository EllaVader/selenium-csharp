using JRoeSelenium.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace JRoeSelenium.Pages
{
    /// <summary>
    /// Base Page that all pages inherit.  Inherits from <typeparamref name="LoadableComponent"/>
    /// Which is used to ensure the page is loaded before continuing.
    /// </summary>
    /// <typeparam name="T">Type of page being instantiated</typeparam>
    public abstract class BasePage<T> : LoadableComponent<BasePage<T>>
    {
        protected IWebDriver Driver;
        protected string PageTitle;

        // Shared Page Elements
        protected By MessageBar => By.Id("flash");
        protected By CloseMessage => By.CssSelector("a.close");

        protected BasePage(IWebDriver driver, string pageTitle)
        {
            this.Driver = driver;
            this.PageTitle = pageTitle;
            // calls inherited class EvaluateLoadStatus
            Load();
        }

        /// <summary>
        /// Returns true if the message bar on the page is displayed, false otherwise
        /// </summary>
        /// <returns>true if displayed false if not displayed</returns>
        public bool IsMessageDisplayed()
        {
            try
            {
                IWebElement messageBar = Driver.FindElement(MessageBar);
                return messageBar != null ? true : false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the Message Bar is a success message.  
        /// </summary>
        /// <returns>true if success message otherwise false</returns>
        public bool IsSuccessMessage()
        {
            IWebElement messageBar = Driver.FindElement(MessageBar);
            string classAttribute = messageBar.GetAttribute("class");
            bool isSuccess = classAttribute.Contains("success");
            return isSuccess;
        }

        /// <summary>
        /// Returns true if Message Bar is an error message.
        /// </summary>
        /// <returns>true if error message otherwise false</returns>
        public bool IsErrorMessage()
        {
            IWebElement messageBar = Driver.FindElement(MessageBar);
            string classAttribute = messageBar.GetAttribute("class");
            bool isSuccess = classAttribute.Contains("error");
            return isSuccess;
        }

        /// <summary>
        /// Gets the contents of the Message Bar
        /// </summary>
        /// <returns>Message Bar contents</returns>
        public string GetMessageBarMessage()
        {
            IWebElement messageBar = Driver.FindElement(MessageBar);
            string message = messageBar.Text.TrimEnd();
            return message;
        }

        /// <summary>
        /// Clears the Message Bar by closing it with the "x" icon
        /// </summary>
        public void ClearMessage()
        {
            if (IsMessageDisplayed())
            {
                IWebElement messageBar = Driver.FindElement(MessageBar);
                TestContext.Out.WriteLine("Closing Message Bar");
                Driver.FindElement(CloseMessage).Click();
                new WaitUtil(Driver).WaitForElementGone(MessageBar);
            }
            else
            {
                TestContext.Out.WriteLine("Message Bar is not displayed.");
            }

        }

        protected override void ExecuteLoad()
        {
            PageFactory.InitElements(Driver, this);
        }

        protected override bool EvaluateLoadedStatus()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                bool found = wait.Until(d => d.Title.Contains(PageTitle));
                TestContext.Write($"Page {PageTitle} loaded successfully");
                return found;

            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Page {PageTitle} did not load. {ex.Message}");
                return false;
            }
        }
    }
}
