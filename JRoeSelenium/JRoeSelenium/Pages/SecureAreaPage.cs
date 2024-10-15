using JRoeSelenium.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JRoeSelenium.Pages
{
    /// <summary>
    /// Represents the Secure Area Page after a sucessful login
    /// </summary>
    public class SecureAreaPage : BasePage<SecureAreaPage>
    {

        private const string Title = "The Internet";

        // Page Elements
        private By LoginPageHeader => By.XPath("//h2[contains(text(),'Secure Area')]");
        private By LogoutButton => By.XPath("//a/i[contains(text(),'Logout')]");

        public SecureAreaPage(IWebDriver driver) : base(driver, Title)
        { }

        /// <summary>
        /// Logout from the site by clicking on the Logout Button
        /// </summary>
        /// <returns><typeparamref name="LoginPage"/></returns>
        public LoginPage Logout()
        {
            TestContext.Out.WriteLine("Clicking Logout button");
            Driver.FindElement(LogoutButton).Click();

            LoginPage loginPage = new LoginPage(Driver);
            return loginPage;

        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                new WaitUtil(Driver).WaitForElementVisible(LoginPageHeader);
                TestContext.Out.WriteLine("Secure Area Page loaded successfully");
                return true;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine($"Unable to load SecureAreaPage: {ex.Message}");
                if (ex.InnerException != null)
                {
                    TestContext.Error.WriteLine(ex.InnerException.Message);
                }
                return false;
            }

        }
    }
}
