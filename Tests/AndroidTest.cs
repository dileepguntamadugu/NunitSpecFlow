using Allure.NUnit;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using HybridTest.Session;
using OpenQA.Selenium;


namespace HybridTest.Tests
{
    [TestFixture]
    [Category("Hybrid")]
    [AllureNUnit]
    public class AndroidTest 
    {
        private AppiumOptions options;
        protected AppiumDriver driver; 
        
        [SetUp]
        public void Setup()
        {
            var testExecutionContext = new TestExecutionContext();
            options = testExecutionContext.AppiumOptions;
            driver = new AndroidDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub"), options);
            Console.WriteLine("Setup");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose(); 
        }

        [Test]
        public void OpenNewsApp()
        {
            Console.WriteLine("TestMethod");
            if (driver == null)
            {
                throw new InvalidOperationException("Driver is not initialized. Check the setup process.");
            }        
            AppiumElement skipElement = driver.FindElement(By.XPath("//android.widget.TextView[@text=\"Skip\"]"));
            if (skipElement == null)
            {
                Console.WriteLine("SkipElement is null");
            }
            skipElement.Click();
        }
    }
}