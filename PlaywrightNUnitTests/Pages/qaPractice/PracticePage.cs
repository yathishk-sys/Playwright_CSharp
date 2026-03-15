using Microsoft.Playwright;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;

namespace PlaywrightNUnitTests.Pages.qaPractice;

public class PracticePage
{
    private readonly IPage _page;
    private ILocator NewTabLink => _page.GetByRole(AriaRole.Link, new() { Name = "New tab" });
    private ILocator NewBrowserTabLink => _page.Locator("#new-page-link");
    private ILocator NewTabText => _page.GetByText("I am a new page in a new tab");
    private ILocator SwitchToNewWindowButton => _page.GetByText("Switch To New Window");
    private ILocator AlertLink => _page.GetByRole(AriaRole.Link, new() { Name = "Alert" });
    private ILocator AlertButton => _page.GetByRole(AriaRole.Link, new() { Name = "Click" });
    private Utils Utils;
    public PracticePage(IPage page)
    {
        _page = page;
        Utils = new Utils(_page);
    }

    public async Task ClickNewTabLink()
    {
        try
        {
            await Utils.Click(NewTabLink);
            ExtentReportManager.LogPass("New Tab link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("New Tab link was not visible within the timeout period.");
            throw new Exception("New Tab link was not visible within the timeout period.");
        }
    }

    public async Task<IPage> ClickNewBrowserTabLink()
    {
        try
        {
            var newPage = await _page.RunAndWaitForPopupAsync(async () =>
            {
                await Utils.Click(NewBrowserTabLink);
            });
            await newPage.WaitForLoadStateAsync();            
            // await Utils.Click(NewBrowserTabLink);
            ExtentReportManager.LogPass("New Browser Tab link is visible.");
            return newPage;
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("New Browser Tab link was not visible within the timeout period.");
            throw new Exception("New Browser Tab link was not visible within the timeout period.");
        }
    }

    public async Task ClickAlertLink()
    {
        try
        {
            await Utils.Click(AlertLink);
            ExtentReportManager.LogPass("Alert link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Alert link was not visible within the timeout period.");
            throw new Exception("Alert link was not visible within the timeout period.");
        }
    }

    public async Task ClickAlertButton()
    {
        try
        {
            await Utils.Click(AlertButton);
            ExtentReportManager.LogPass("Alert button is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Alert button was not visible within the timeout period.");
            throw new Exception("Alert button was not visible within the timeout period.");
        }
    }

    public async Task HandleAlert()
    {
        try
        {                
            // _page.Dialog += async (_, dialog) =>
            // {
            //     await dialog.AcceptAsync();
            // };
            Utils.AcceptAlert();
            ExtentReportManager.LogPass("Alert is Accepted.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Alert was not visible within the timeout period.");
            throw new Exception("Alert was not visible within the timeout period.");
        }
    }

}