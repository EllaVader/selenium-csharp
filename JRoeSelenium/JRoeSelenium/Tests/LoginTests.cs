using JRoeSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JRoeSelenium.Tests
{
    /// <summary>
    /// Login Tests - verifies login functionality. To allow for cross-browser testing, I am leverging, 
    /// NUnit TestFixture decorator which will pass in the target browser to run the tests.
    /// By doing this, you can test with multiple browsers (i.e. Chrome, Firefox, Edge).
    /// The tests will be run against all browsers defined in each TestFixture.
    /// I am only running in ChromeDriver, but to add another browser, add another TestFixture
    /// decorator at the top of the class
    /// </summary>
    /// <typeparam name="TWebDriver">The browser type to instantiate and run the test</typeparam>
    public class LoginTest<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        public string Username = "tomsmith";
        public string Password = "SuperSecretPassword!";

        [Test]
        public void LoginUser()
        {
            string successMessage = "You logged into a secure area";
            SecureAreaPage secureAreaPage = SuccessfulLogin(Username, Password);

            Assert.That(secureAreaPage.IsSuccessMessage, Is.True, "Message is not a success message");
            string actualMessage = secureAreaPage.GetMessageBarMessage();
            Assert.That(actualMessage, Does.Contain(successMessage), "Message bar message is not correct");
        }

        [Test]
        public void LoginInvalidUsername()
        {
            string errorMessage = "Your username is invalid";

            LoginPage loginPage = new LoginPage(Driver);
            loginPage.LoginWithInvalidCredentials("bogus", Password);
            Assert.That(loginPage.IsErrorMessage, Is.True, "Message is not an error message");
            string actualMessage = loginPage.GetMessageBarMessage();
            Assert.That(actualMessage, Does.Contain(errorMessage), "Message bar message is not correct");
        }

        [Test]
        public void LoginInvalidPassword()
        {
            string errorMessage = "Your password is invalid";

            LoginPage loginPage = new LoginPage(Driver);
            loginPage.LoginWithInvalidCredentials(Username, "bogus");
            Assert.That(loginPage.IsErrorMessage, Is.True, "Message is not an error message");
            string actualMessage = loginPage.GetMessageBarMessage();
            Assert.That(actualMessage, Does.Contain(errorMessage), "Message bar message is not correct");
        }

        [Test]
        public void LogoutUser()
        {
            string logoutMessage = "You logged out of the secure area";
            // call login user
            SecureAreaPage secureAreaPage = SuccessfulLogin(Username, Password);
            LoginPage loginPage = secureAreaPage.Logout();

            Assert.That(loginPage.IsSuccessMessage, Is.True, "Message is not a success message");
            string actualMessage = loginPage.GetMessageBarMessage();
            Assert.That(actualMessage, Does.Contain(logoutMessage), "Message bar message is not correct");
        }

        private SecureAreaPage SuccessfulLogin(string userName, string password)
        {
            LoginPage loginPage = new LoginPage(Driver);
            SecureAreaPage secureAreaPage = loginPage.LoginWithCorrectCredentials(Username, Password);
            return secureAreaPage;
        }


    }
}