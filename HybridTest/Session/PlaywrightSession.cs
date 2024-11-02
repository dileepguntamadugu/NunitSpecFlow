using Microsoft.Playwright;

namespace HybridTest.Session;

public class PlaywrightSession
{
    public async Task<IBrowser> GetPlaywrightSession()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true // Set to false to see the browser UI
        });
        return browser;
    }
}