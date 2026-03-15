using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using System;

namespace PlaywrightNUnitTests.Reports;

/// <summary>
/// Manages ExtentReports for test reporting and documentation.
/// </summary>
public static class ExtentReportManager
{
    private static ExtentReports? _extentReports;
    private static ExtentTest? _extentTest;

    /// <summary>
    /// Initializes the ExtentReports instance.
    /// </summary>
    public static void InitializeReport(string reportPath = "")
    {
        if (_extentReports != null)
            return;

        if (string.IsNullOrEmpty(reportPath))
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); 
            reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports", "ExtentReport" + timestamp + ".html");
            // reportPath = Path.Combine("src", "TestReports", "ExtentReport" + timestamp + ".html");
        }

        // var directory = Path.GetDirectoryName(reportPath);
        // if (!Directory.Exists(directory))
        //     Directory.CreateDirectory(directory);

        var htmlReporter = new ExtentSparkReporter(reportPath);
        htmlReporter.Config.ReportName = "Playwright Test Automation Report";
        htmlReporter.Config.DocumentTitle = "Test Execution Report";
        htmlReporter.Config.Theme = Theme.Standard; 

        _extentReports = new ExtentReports();
        _extentReports.AttachReporter(htmlReporter);
        _extentReports.AddSystemInfo("OS", Environment.OSVersion.ToString());
        _extentReports.AddSystemInfo("Environment", "QA");
    }

    /// <summary>
    /// Creates a new test in the report.
    /// </summary>
    public static void CreateTest(string testName, string description = "")
    {
        _extentTest = _extentReports?.CreateTest(testName, description);
    }

    /// <summary>
    /// Logs information to the current test.
    /// </summary>
    public static void LogInfo(string message)
    {
        _extentTest?.Info(message);
    }

    /// <summary>
    /// Logs a pass status to the current test.
    /// </summary>
    public static void LogPass(string message)
    {
        _extentTest?.Pass(message);
    }

    /// <summary>
    /// Logs a fail status to the current test.
    /// </summary>
    public static void LogFail(string message)
    {
        _extentTest?.Fail(message);
    }

    /// <summary>
    /// Logs a warning to the current test.
    /// </summary>
    public static void LogWarning(string message)
    {
        _extentTest?.Warning(message);
    }

    /// <summary>
    /// Logs a skip status to the current test.
    /// </summary>
    public static void LogSkip(string message)
    {
        _extentTest?.Skip(message);
    }

    /// <summary>
    /// Attaches a screenshot to the current test.
    /// </summary>
    public static void AttachScreenshot(string screenshotPath)
    {
        if (File.Exists(screenshotPath))
            _extentTest?.AddScreenCaptureFromPath(screenshotPath);
    }

    /// <summary>
    /// Flushes and finalizes the report.
    /// </summary>
    public static void FlushReport()
    {
        _extentReports?.Flush();
    }

    /// <summary>
    /// Gets the current ExtentTest instance.
    /// </summary>
    public static ExtentTest? GetCurrentTest()
    {
        return _extentTest;
    }

    /// <summary>
    /// Ends the current test in the report.
    /// </summary>
    public static void EndTest()
    {
        _extentTest = null;
    }
}