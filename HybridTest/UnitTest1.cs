using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace HybridTest
{
    public class NunitPlaywright : PageTest
    {
        private IBrowser _browser;
        private IPlaywright _playwright;
        
        [SetUp]
        public async Task Setup()
        {
            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true // Set to false to see the browser UI
            });
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
        }

        [Test]
        public async Task NavigateToGoogleHomePage()
        {

            // Use the already initialized _page
            await Page.GotoAsync("https://www.google.com/");
            Console.WriteLine(await Page.TitleAsync());
            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "before.jpg"
            });
            await Page.FillAsync("[id='APjFqb']","What is Playwright?");
            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "after.jpg"
            });

            // Example assertion to verify navigation
            Assert.IsTrue((await Page.TitleAsync()).Contains("Google"));
        }
    }
}