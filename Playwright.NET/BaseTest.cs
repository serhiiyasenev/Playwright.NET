using Microsoft.Playwright;

namespace Playwright.NET
{
    public class BaseTest
    {
        private static string basePath => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
        private static string downloadsPath => Path.Combine(basePath, "Downloads");
        private static string videoPath => Path.Combine(basePath, "Video");

        public IPlaywright Playwright { get; set; } = null!;
        public IBrowser Browser { get; set; } = null!;
        public IBrowserContext Context { get; set; } = null!;
        public IPage Page { get; set; } = null!;

        public BrowserNewContextOptions ContextOptions() => new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport,
            IgnoreHTTPSErrors = true,
            // RecordVideoDir = videoPath,
            // RecordVideoSize = new RecordVideoSize { Width = 640, Height = 480 },
        };

        [SetUp]
        public async Task BrowserSetup()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--start-maximized" },
                //DownloadsPath = downloadsPath,
            });
            Context = await Browser.NewContextAsync(ContextOptions());
            Page = await Context.NewPageAsync();

            // await Context.Tracing.StartAsync(new TracingStartOptions
            // {
            //     Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            //     Screenshots = true,
            //     Snapshots = true,
            //     Sources = true
            // });
        }

        [TearDown]
        public async Task BrowserTearDown()
        {
            // var tracePath = Path.Combine(
            //    basePath,
            //    "playwright-traces",
            //    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip");
            // await Context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath });

            await Page.CloseAsync();
            await Context.DisposeAsync();
            await Browser.DisposeAsync();
            Playwright.Dispose();

            Page = null!;
            Context = null!;
            Browser = null!;
            Playwright = null!;
        }
    }
}
