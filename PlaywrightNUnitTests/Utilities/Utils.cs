using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightNUnitTests.Utilities;

public class Utils
{
    private readonly IPage _page;

    public Utils(IPage page)
    {
        _page = page;
    }

    // Navigate
    public async Task Navigate(string url)
    {
        await _page.GotoAsync(url);
    }

    // Click
    public async Task Click(ILocator locator)
    {
        await WaitForElement(locator);
        await locator.ClickAsync();
    }

    // Type
    public async Task Type(ILocator locator, string text)
    {
        await WaitForElement(locator);
        await locator.FillAsync(text);
    }

    // Clear + Type
    public async Task ClearAndType(ILocator locator, string text)
    {
        await locator.FillAsync("");
        await locator.FillAsync(text);
    }

    // Get text
    public async Task<string> GetText(ILocator locator)
    {
        return await locator.InnerTextAsync();
    }

    // Wait for element
    public async Task WaitForElement(ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible           
        });
    }

    // Check visibility
    public async Task<bool> IsVisible(ILocator locator)
    {
        return await locator.IsVisibleAsync();
    }

    // Dropdown by value
    public async Task SelectByValue(ILocator locator, string value)
    {
        await locator.SelectOptionAsync(value);
    }

    // Dropdown by label
    public async Task SelectByText(ILocator locator, string text)
    {
        await locator.SelectOptionAsync(new SelectOptionValue { Label = text });
    }

    // Scroll into view
    public async Task ScrollIntoView(ILocator locator)
    {
        await locator.ScrollIntoViewIfNeededAsync();
    }

    // Hover
    public async Task Hover(ILocator locator)
    {
        await locator.HoverAsync();
    }

    // Double click
    public async Task DoubleClick(ILocator locator)
    {
        await locator.DblClickAsync();
    }

    // Right click
    public async Task RightClick(ILocator locator)
    {
        await locator.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });
    }

    // Press keyboard key
    public async Task PressKey(string key)
    {
        await _page.Keyboard.PressAsync(key);
    }

    // Drag and drop
    public async Task DragAndDrop(ILocator source, ILocator target)
    {
        await source.DragToAsync(target);
    }

    // Upload file
    public async Task UploadFile(ILocator locator, string filePath)
    {
        await locator.SetInputFilesAsync(filePath);
    }

    // Screenshot
    public async Task TakeScreenshot(string name)
    {
        await _page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"Screenshots/{name}.png",
            FullPage = true
        });
    }

    // Refresh page
    public async Task Refresh()
    {
        await _page.ReloadAsync();
    }

    // Wait for page load
    public async Task WaitForPageLoad()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    // Switch to iframe
    public IFrame SwitchToFrame(string frameName)
    {
        return _page.Frame(frameName);
    }

    // Handle alert accept
    public void AcceptAlert()
    {
        _page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };
    }

    // Handle alert dismiss
    public void DismissAlert()
    {
        _page.Dialog += async (_, dialog) =>
        {
            await dialog.DismissAsync();
        };
    }

    //Handle prompt alert
    public void HandlePrompt(string input)
    {
        _page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync(input);
        };
    }

    // JavaScript click
    public async Task JsClick(ILocator locator)
    {
        await _page.EvaluateAsync("(element) => element.click()", locator);
    }

    // Scroll down
    public async Task ScrollDown()
    {
        await _page.Mouse.WheelAsync(0, 600);
    }

    // Scroll to bottom
    public async Task ScrollToBottom()
    {
        await _page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
    }

    //Download file
    public async Task<string> DownloadAsync(string clickSelector, string downloadFolder)
        {
            var download = await _page.RunAndWaitForDownloadAsync(async () =>
            {
                await _page.ClickAsync(clickSelector);
            });

            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);

            var filePath = Path.Combine(downloadFolder, download.SuggestedFilename);

            await download.SaveAsAsync(filePath);

            return filePath;
        }

    //Parallel execution helper
    //dotnet test -- NUnit.NumberOfTestWorkers=4
}