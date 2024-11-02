using Microsoft.Playwright;
using Newtonsoft.Json;

namespace HybridTest.Session;

public class BrowserStackSession
{
    public async Task<IBrowser> GetBrowserStackSession()
    {
        var playwright = await Playwright.CreateAsync();
        Dictionary<string, string> browserstackOptions = new Dictionary<string, string>();
        browserstackOptions.Add("name", "Playwright first sample test");
        browserstackOptions.Add("build", "Test_" + DateTime.Now.ToString("ddMMyyyy_HH_mm"));
        browserstackOptions.Add("os", "osx");
        browserstackOptions.Add("os_version", "catalina");
        browserstackOptions.Add("browser", "chrome");  // allowed browsers are `chrome`, `edge`, `playwright-chromium`, `playwright-firefox` and `playwright-webkit`
        /*browserstackOptions.Add("browserstack.username", "dileepguntamadug_N1qwrS");
        browserstackOptions.Add("browserstack.accessKey", "4u2mCcWeUVpM7jkpWzDo");*/
        browserstackOptions.Add("browserstack.username", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        browserstackOptions.Add("browserstack.accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));
        string capsJson = JsonConvert.SerializeObject(browserstackOptions);
        string cdpUrl = "wss://cdp.browserstack.com/playwright?caps=" + Uri.EscapeDataString(capsJson);
        return await playwright.Chromium.ConnectAsync(cdpUrl);
    }
}