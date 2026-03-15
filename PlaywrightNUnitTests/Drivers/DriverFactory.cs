using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightNUnitTests.Drivers;

public class DriverFactory
{
    private static IPlaywright _playwright;

    private static IBrowser _browser = null;
    private static IBrowserContext _context = null;
    private static IPage _page = null;

    public static async Task InitBrowser(string browserType)
    {
        if (_playwright == null)
            _playwright = await Playwright.CreateAsync();

        _browser = browserType.ToLower() switch
        {
            "chromium" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50
            }),

            "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            }),

            "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            }),

            _ => throw new ArgumentException($"Unsupported browser: {browserType}")
        };

        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
            IgnoreHTTPSErrors = true
        });

        // Start tracing
        await _context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        _page = await _context.NewPageAsync();
    }

    public static IPage GetPage()
    {
        if (_page == null)
            throw new Exception("Page not initialized. Call InitBrowser first.");

        return _page;
    }

    public static IBrowserContext GetContext()
    {
        return _context;
    }

    public static async Task CloseBrowser()
    {
        if (_context != null)
        {
            var testName = TestContext.CurrentContext.Test.Name;

            // Save trace file
            await _context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"Traces/{testName}.zip"
            });

            await _context.CloseAsync();
        }

        if (_browser != null)
            await _browser.CloseAsync();
    }
}