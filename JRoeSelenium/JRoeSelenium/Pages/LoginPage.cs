using JRoeSelenium.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JRoeSelenium.Pages
{
    /// <summary>
    /// Represents the Login Page on The Internet website
    /// </summary>
    public class LoginPage : BasePage<LoginPage>
    {
        private const string Title = "The Internet";

        // Page Elements
        private By LoginPageHeader => By.XPath("//h2[contains(text(),'Login Page')]");
        private By UserNameTextbox => By.Id("username");
        private By PasswordTextbox => By.Id("password");
        private By LoginButton => By.XPath("//button/i[contains(text(),'Login')]");

        public LoginPage(IWebDriver driver) : base(driver, Title)
        { }

        /// <summary>
        /// Login to the webite with correct usernname and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SecureAreaPage LoginWithCorrectCredentials(string username, string password)
        {
            TestContext.Out.WriteLine($"Logging in with username: {username} and password: {password}");
            Driver.FindElement(UserNameTextbox).SendKeys(username);
            Driver.FindElement(PasswordTextbox).SendKeys(password);
            Driver.FindElement(LoginButton).Click();

            SecureAreaPage secureAreaPage = new SecureAreaPage(Driver);
            return secureAreaPage;
        }

        /// <summary>
        /// Attempt to login to the website with invalid credintials.
        /// Waits for the message bar to display with an error message before returning
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LoginWithInvalidCredentials(string username, string password)
        {
            TestContext.Out.WriteLine($"Logging in with username: {username} and password: {password}");
            Driver.FindElement(UserNameTextbox).SendKeys(username);
            Driver.FindElement(PasswordTextbox).SendKeys(password);
            Driver.FindElement(LoginButton).Click();
            new WaitUtil(Driver).WaitForElementVisible(MessageBar);
        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                new WaitUtil(Driver).WaitForElementVisible(LoginPageHeader);
                TestContext.Out.WriteLine("Login Page loaded successfully");
                return true;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine($"Unable to load LoginPage: {ex.Message}");
                if (ex.InnerException != null)
                {
                    TestContext.Error.WriteLine(ex.InnerException.Message);
                }
                return false;
            }
        }


    }
}
