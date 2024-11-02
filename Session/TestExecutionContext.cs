using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Playwright;

namespace HybridTest.Session
{
    public class TestExecutionContext
    {
        public string BaseUrl { get; private set; }
        public int Timeout { get; private set; }
        public string TestExecutionEnvironment { get; private set; }
        public Task<IBrowser> Browser { get; private set; }
        private PlaywrightSession _playwrightSession = new PlaywrightSession();
        private BrowserStackSession _browserStackSession = new BrowserStackSession();

        public TestExecutionContext()
        {
            var environment = TestContext.Parameters["Environment"] ?? "uat_bstack";  // Default to "UAT"
            var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"Configuration/TestExecutionContext/{environment}.json");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                .Build();
            BaseUrl = configuration["baseURL"];
            TestExecutionEnvironment = configuration["test_execution_environment"] ?? "browserstack"; //defaults to execute on Browserstack
            Browser = TestExecutionEnvironment.ToLower().Equals("playwright") ? _playwrightSession.GetPlaywrightSession() : _browserStackSession.GetBrowserStackSession();
        }
    }
}