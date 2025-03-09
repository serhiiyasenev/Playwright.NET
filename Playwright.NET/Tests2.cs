namespace Playwright.NET
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests2 : BaseTest
    {
        [Test]
        public async Task HomePageHasPlaywrightInTitleAndGetStartedLinkLinkingToTheIntroPage()
        {
            await Page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            var title = await Page.TitleAsync();
            Assert.That(title.Contains("Playwright"));

            // create a locator
            var getStarted = Page.Locator("text=Get Started");
            var href = await getStarted.GetAttributeAsync("href");

            // Expect an attribute "to be strictly equal" to the value.
            Assert.That(href!.Equals("/docs/intro"));

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects the URL to contain intro.
            var currentUrl = Page.Url;
            Thread.Sleep(2000); // for Video
            Assert.That(currentUrl.Equals("https://playwright.dev/docs/intro"));
        }
    }
}
