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
    private ILocator Formslink => _page.GetByRole(AriaRole.Link, new() { Name = "Forms" });
    private ILocator PracticeFormLink => _page.GetByRole(AriaRole.Link, new() {Name="Practice Form"});
    private ILocator FirstNameInput => _page.GetByLabel("First Name");
    private ILocator LastNameInput => _page.GetByLabel("Last Name");
    private ILocator EmailInput => _page.GetByLabel("Email");
    private ILocator ChooseFileIcon => _page.Locator("#uploadPicture");
    private ILocator SelectLink => _page.Locator("//a[text()='Select']");
    private ILocator SelectValueLink => _page.GetByLabel("Choose language", new() { Exact = true });
    // private ILocator DropdownOptions => _page.GetB
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

    public async Task ClickFormsLink()
    {
        try
        {
            await Utils.Click(Formslink);
            ExtentReportManager.LogPass("Forms link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Forms link was not visible within the timeout period.");
            throw new Exception("Forms link was not visible within the timeout period.");
        }
    }

    public async Task ClickPracticeFormLink()
    {
        try
        {
            await Utils.Click(PracticeFormLink);
            await _page.WaitForSelectorAsync("text=Practice Form",
             new PageWaitForSelectorOptions { Timeout = 5000 });
            ExtentReportManager.LogPass("Practice Form link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Practice Form link was not visible within the timeout period.");
            throw new Exception("Practice Form link was not visible within the timeout period.");
        }
    }

    public async Task ClickChooseFileIcon()
    {
        try
        {
            await Utils.Click(ChooseFileIcon);
            await _page.WaitForTimeoutAsync(2000); // Wait for 2 seconds to ensure the file dialog is open
            await Utils.UploadFile(ChooseFileIcon, "C:\\Users\\Laptop\\Pictures\\Screenshots\\test.png");
            ExtentReportManager.LogPass("Choose File icon is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Choose File icon was not visible within the timeout period.");
            throw new Exception("Choose File icon was not visible within the timeout period.");
        }
    }

    public async Task FillFirstName(string firstName)
    {
        try
        {
            await Utils.ClearAndType(FirstNameInput, firstName);
            ExtentReportManager.LogPass("First Name is filled.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("First Name input was not visible within the timeout period.");
            throw new Exception("First Name input was not visible within the timeout period.");
        }
    }

    public async Task FillLastName(string lastName)
    {
        try
        {
            await Utils.ClearAndType(LastNameInput, lastName);
            ExtentReportManager.LogPass("Last Name is filled.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Last Name input was not visible within the timeout period.");
            throw new Exception("Last Name input was not visible within the timeout period.");
        }
    }

    public async Task FillEmail(string email)
    {
        try
        {
            await Utils.ClearAndType(EmailInput, email);
            ExtentReportManager.LogPass("Email is filled.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Email input was not visible within the timeout period.");
            throw new Exception("Email input was not visible within the timeout period.");
        }
    }

    public async Task ClickSelectLink()
    {
        try
        {
            await Utils.Click(SelectLink);
            ExtentReportManager.LogPass("Select link is visible.");
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Select link was not visible within the timeout period.");
            throw new Exception("Select link was not visible within the timeout period.");
        }
    }

    public async Task GetDropdownValues()
    {
        try
        {
            // await Utils.Click(SelectValueLink);
            var options = await _page.Locator("//select[@name='choose_language']/option").AllTextContentsAsync();
            Console.WriteLine("Dropdown options:");
            Array.Sort((Array)options);            
            options.ToList().ForEach(option => Console.WriteLine(option));
            options.Where(option => option.Contains("av")).ToList().ForEach(option => Console.WriteLine("Option containing 'av': " + option));
            ExtentReportManager.LogPass("Select Value link is visible."+options.ToList());
        }
        catch (TimeoutException)
        {
            ExtentReportManager.LogFail("Select Value link was not visible within the timeout period.");
            throw new Exception("Select Value link was not visible within the timeout period.");
        }
    }


}