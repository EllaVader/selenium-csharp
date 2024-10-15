using JRoeSelenium.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JRoeSelenium.Tests
{
    //[Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(typeof(ChromeDriver))]
    public abstract class TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            string baseUrl = TestHelper.GetTestData("BaseUrl");
            Driver = new TWebDriver();//new ChromeDriver();
            Console.Out.WriteLine($"Starting {DriverHelper.GetDriverName(Driver)} browser");
            Driver.Url = baseUrl;
        }

        [TearDown]
        public void Teardown()
        {
            Console.Out.WriteLine($"Finishing Test: {TestContext.CurrentContext.Test.Name}");
            Console.Out.WriteLine($"Closing {DriverHelper.GetDriverName(Driver)} browser");
            Driver.Quit();
        }

    }
}
