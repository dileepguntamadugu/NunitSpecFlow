using Microsoft.Playwright;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;

namespace HybridTest.Session;

public class BrowserStackSession
{
    public string platformName { get; set; }                                                                                                                        
    public string deviceName { get; set; }                                                                                                                          
    public string app { get; set; }   
    public async Task<IBrowser> GetBrowserStackSession()
    {
        var playwright = await Playwright.CreateAsync();
        Dictionary<string, string> browserstackOptions = new Dictionary<string, string>();
        browserstackOptions.Add("name", "Playwright first sample test");
        browserstackOptions.Add("build", "Test_" + DateTime.Now.ToString("ddMMyyyy_HH_mm"));
        browserstackOptions.Add("os", "osx");
        browserstackOptions.Add("os_version", "catalina");
        browserstackOptions.Add("browser", "chrome");  // allowed browsers are `chrome`, `edge`, `playwright-chromium`, `playwright-firefox` and `playwright-webkit`
        browserstackOptions.Add("browserstack.username", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        browserstackOptions.Add("browserstack.accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));
        string capsJson = JsonConvert.SerializeObject(browserstackOptions);
        string cdpUrl = "wss://cdp.browserstack.com/playwright?caps=" + Uri.EscapeDataString(capsJson);
        return await playwright.Chromium.ConnectAsync(cdpUrl);
    }

    public AppiumOptions GetAndroidBrowserStackSession()
    {
        AppiumOptions capabilities = new AppiumOptions();
        Dictionary<string, string> bstackOptions = new Dictionary<string, string>();
        bstackOptions.Add("userName", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        bstackOptions.Add("accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));
        capabilities.AddAdditionalAppiumOption("platformName", "android");
        capabilities.AddAdditionalAppiumOption("PlatformVersion", "12.0");
        capabilities.AddAdditionalAppiumOption("DeviceName", "Samsung Galaxy S22 Ultra");
        capabilities.AddAdditionalAppiumOption("Application","bs://6dd6787850cfd416d737401ba631a45ee3056ecd");
        capabilities.AddAdditionalAppiumOption("browserstack.user", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        capabilities.AddAdditionalAppiumOption("browserstack.key", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));
        capabilities.AddAdditionalAppiumOption("bstack:options", bstackOptions);
        //bs://51e2fbf619a3315cf0213926ff4a97378f8ee470
        return capabilities;
    }
}