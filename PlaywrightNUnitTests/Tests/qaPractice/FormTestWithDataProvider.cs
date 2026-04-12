using System;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightNUnitTests.Drivers;
using PlaywrightNUnitTests.Pages.qaPractice;
using PlaywrightNUnitTests.Reports;
using PlaywrightNUnitTests.testsource;

namespace PlaywrightNUnitTests.Tests.qaPractice;

public class FormTestWithDataProvider : BaseTest
{
    private IPage page;

    [SetUp]
    public async Task Setup()
    {
        page = DriverFactory.GetPage();
        ExtentReportManager.CreateTest("Form Test with Data Provider");
        await page.GotoAsync("https://www.qa-practice.com/");
    }

    [TestCase("John", "Doe", "john.doe@example.com")]
    [Test]
    public async Task TestFormSubmission(string firstName, string lastName, string email)
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickFormsLink();
        await practicePage.ClickPracticeFormLink();

        await practicePage.FillFirstName(firstName);
        await practicePage.FillLastName(lastName);
        await practicePage.FillEmail(email);

        // Add assertions to verify form submission if needed
    }

    // [TestCase("John", "Doe", "john.doe@example.com")]
    [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.GetLoginData))]
    [Parallelizable(ParallelScope.All)]
    public async Task TestFormSubmissionWithTestSource(LoginData data)
    {
        var homePage = new HomePage(page);
        await homePage.Init();
        await homePage.ClickSingleUIElements();

        var practicePage = new PracticePage(page);
        await practicePage.ClickFormsLink();
        await practicePage.ClickPracticeFormLink();

        await practicePage.FillFirstName(data.firstname);
        await practicePage.FillLastName(data.lastname);
        await practicePage.FillEmail(data.email);

        // Add assertions to verify form submission if needed
    }

    [TearDown]
    public async Task TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            ExtentReportManager.LogPass("Test passed.");
        }
        else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {   
            var screenshotPath = $"Screenshots/{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
            ExtentReportManager.LogFail($"Test failed. Screenshot saved at: {screenshotPath}");
            // await ExtentReportManager.LogFail("Test failed.");
        }
    }
}