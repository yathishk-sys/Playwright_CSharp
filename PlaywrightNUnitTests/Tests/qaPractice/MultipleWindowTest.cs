using NUnit.Framework;
using Microsoft.Playwright;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Pages.qaPractice;
using PlaywrightNUnitTests.Reports;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightNUnitTests.Tests.qaPractice;

public class MultipleWindowTest : BaseTest
{
    private IPage page;

    [SetUp]
    public async Task Setup()
    {
        page = DriverFactory.GetPage();

        ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);

        await page.GotoAsync("https://www.qa-practice.com/");
    }

    [Test]
    public async Task TestMultipleWindow()
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickNewTabLink();

        // await practicePage.ClickNewBrowserTabLink(); 

        var newpage = await page.RunAndWaitForPopupAsync(async () =>
        {
            await page.Locator("#new-page-link").ClickAsync();
        });

        IPage targetPage = null; // Assuming the new tab is the target page
        foreach (var p in page.Context.Pages)
        {
            if(await p.GetByText("I am a new page in a new tab").IsVisibleAsync())
            {
                targetPage = p;
                break;
            }
        }
        await targetPage.BringToFrontAsync();
        await Expect(targetPage.GetByText("I am a new page in a new tab")).ToBeVisibleAsync();        
    }

    [TearDown]
    public void TestTearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            ExtentReportManager.LogPass("Test Passed");
        }
        else
        {
            ExtentReportManager.LogFail("Test Failed");
        }
    }
}