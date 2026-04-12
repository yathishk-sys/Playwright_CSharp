using System;
using System.Threading.Tasks;
using PlaywrightNUnitTests.Utilities;
using Microsoft.Playwright;
using PlaywrightNUnitTests.Reports;

namespace PlaywrightNUnitTests.Pages.qaPractice;
public class ShadowDOMPractice
{
    private readonly IPage _page;
    private ILocator userNameElement => _page.Locator("#userName").Nth(0).Locator(".kils");
    private Utils Utils;

    public ShadowDOMPractice(IPage page)
    {
        _page = page;
        Utils = new Utils(_page);
    }

    public async Task inputUserName(string userName)
    {
        try
        {
            await Utils.Type(userNameElement, userName);
            ExtentReportManager.LogPass("User Name element is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("User Name element was not visible within the timeout period.");
            throw new Exception("User Name element was not visible within the timeout period.");
        }
    }
}