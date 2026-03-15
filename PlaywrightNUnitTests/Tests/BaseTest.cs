using NUnit.Framework;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Reports;

namespace PlaywrightNUnitTests.Tests;
public class BaseTest
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        ExtentReportManager.InitializeReport();
    }

    [SetUp]
    public async Task SetupBrowser()
    {
        await DriverFactory.InitBrowser("chromium");
    }

    [TearDown]
    public async Task CloseBrowser()
    {
        await DriverFactory.CloseBrowser();
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        ExtentReportManager.FlushReport();
    }
}