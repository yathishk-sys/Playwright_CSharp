using System;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightNUnitTests.Utilities;
using NUnit.Framework;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.Tests;

namespace PlaywrightNUnitTests.Pages.qaPractice;
public class ShawdowDOMPractice : BaseTest
{
    private IPage page;
    
    [SetUp]
    public async Task Setup()
    {
        page = DriverFactory.GetPage();
        await page.GotoAsync("https://selectorshub.com/xpath-practice-page/?utm_source=chatgpt.com");
    }

    [Test]
    public async Task TestShadowDOM()
    {
        var shadowDOMPractice = new ShadowDOMPractice(page);
        await shadowDOMPractice.inputUserName("John Doe");
        await Task.Delay(2000); // Just to see the input before closing the browser        
    }

[TearDown]
    public void TestTearDown()
    {
        // Cleanup code if needed
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if(status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            ExtentReportManager.LogPass("Test Passed");
        }
        else
        {
            string screenshotPath = $"Screenshots/{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";   
            page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true 
            });
            
            ExtentReportManager.LogFailWithScreenshot("Test Failed", screenshotPath);
        }
    }
}
    