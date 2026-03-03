using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightNUnitTests.Tests;

[TestFixture]
public class ExamplePlaywrightTests
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;

    [SetUp]
    public async Task SetUpAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var context = await _browser.NewContextAsync();
        _page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await _page.Context.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [Test]
    public async Task HomepageTitle_ShouldContainPlaywright()
    {
        await _page.GotoAsync("https://playwright.dev");

        var title = await _page.TitleAsync();

        Assert.That(title, Does.Contain("Playwright"));
    }
}
