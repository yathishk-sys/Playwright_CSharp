using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.Tests;
using System.Threading.Tasks;
using PlaywrightNUnitTests.Pages.qaPractice;

namespace PlaywrightNUnitTests.Tests.qaPractice;

public class FileUploadTest : BaseTest
{
    private IPage page;
    [SetUp]
    public async Task SetUp()
    {
        page = DriverFactory.GetPage();
        ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
        await page.GotoAsync("https://www.qa-practice.com/");        
    }

    [Test]
    public async Task TestFileUpload()
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickFormsLink();
        await practicePage.ClickPracticeFormLink();
        await practicePage.ClickChooseFileIcon();
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            ExtentReportManager.LogPass("Test passed.");
        }
        else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            ExtentReportManager.LogFail("Test failed.");
        }
    }
}