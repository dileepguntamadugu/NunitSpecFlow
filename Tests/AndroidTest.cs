using System.Text.RegularExpressions;
using Allure.Net.Commons;
using HybridTest.Session;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Allure.NUnit;


namespace HybridTest.Tests
{
    [TestFixture]
    [Category("Hybrid")]
    [AllureNUnit]
    public class NunitPlaywright : PageTest
    {
        private IBrowser _browser;
        private IPage _page;
        
        [SetUp]
        public async Task Setup()
        {
            var testExecutionContext = new TestExecutionContext();
            _browser = await testExecutionContext.Browser;
            TestContext.Progress.WriteLine($"Running tests against {testExecutionContext.BaseUrl} with timeout {testExecutionContext.Timeout}");
            _page = await _browser.NewPageAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Capture screenshot on failure
                string screenshotPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"screenshot_{TestContext.CurrentContext.Test.Name}.png");
                await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
                // Attach screenshot to Allure report
                AllureApi.AddAttachment(
                    "Screenshot on failure", 
                    "image/png", 
                    screenshotPath);
            }
            await _browser.CloseAsync();
        }

        [Test]
        public async Task NavigateToGoogleHomePage()
        {
            // Use the already initialized _page
            await _page.GotoAsync("https://www.google.com/");
            Console.WriteLine(await _page.TitleAsync());
            await _page.FillAsync("[id='APjFqb']","What is Playwright?");
            await _page.Keyboard.PressAsync("Enter");
            // Example assertion to verify navigation
            await Expect(_page).ToHaveTitleAsync(new Regex("What is Playwright?"));
        }
    }
}