using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightNUnitTests.Drivers; 
using PlaywrightNUnitTests.Pages.qaPractice;
using PlaywrightNUnitTests.Reports;

namespace PlaywrightNUnitTests.Tests.qaPractice;

public class AlertTest : BaseTest
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
    public async Task TestAlert()
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickAlertLink();

        await practicePage.ClickAlertButton();
        await practicePage.HandleAlert();
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