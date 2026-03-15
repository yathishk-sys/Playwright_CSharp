using Microsoft.Playwright;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.Utilities;

namespace PlaywrightNUnitTests.Pages.qaPractice;

public class HomePage
{
    private readonly IPage _page;
    private ILocator SingleUIElements => _page.GetByText("Single UI Elements");
    private Utils Utils;

    public HomePage(IPage page)
    {
        _page = page;
        Utils = new Utils(_page);
        // WaitForSingleUIElements().GetAwaiter().GetResult();        
    }

    public async Task Init()
    {
        await Utils.WaitForElement(SingleUIElements);
    }

    public async Task Navigate(string url)
    {

        try
        {
            await Utils.Navigate(url);
            ExtentReportManager.LogPass($"Navigated to {url} successfully.");
        }
        catch (Exception ex)
        {
            ExtentReportManager.LogFail($"Failed to navigate to {url}. Exception: {ex.Message}");
            throw new Exception($"Failed to navigate to {url}. Exception: {ex.Message}");
        }
    }

    public async Task ClickSingleUIElements()
    {
        try
        {            
            await Utils.Click(SingleUIElements);
            ExtentReportManager.LogPass("Single UI Elements link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Single UI Elements link was not visible within the timeout period.");
            throw new Exception("Single UI Elements link was not visible within the timeout period.");
        }        
    }
}