using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.Tests;
using System.Threading.Tasks;
using PlaywrightNUnitTests.Pages.qaPractice;

namespace PlaywrightNUnitTests.Tests.qaPractice;

public class DropdownTest : BaseTest
{
    private IPage page;

    [SetUp]
    public async Task Setup()
    {
        page = DriverFactory.GetPage();
        await page.GotoAsync("https://www.qa-practice.com/");
        ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
    }

    [Test]
    public async Task TestDropdown()
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickSelectLink();
        await practicePage.GetDropdownValues();
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
