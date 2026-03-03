# Playwright_CSharp

C#/.NET Playwright UI testing template using **NUnit**.

## Project Structure

- `PlaywrightNUnitTests/PlaywrightNUnitTests.csproj` - NUnit test project with Playwright dependencies.
- `PlaywrightNUnitTests/Tests/ExamplePlaywrightTests.cs` - Starter end-to-end UI test.

## Prerequisites

Install the following on your machine:

1. [.NET 8 SDK](https://dotnet.microsoft.com/download)
2. PowerShell (for Playwright browser install script)

## Restore dependencies

```bash
dotnet restore PlaywrightNUnitTests/PlaywrightNUnitTests.csproj
```

## Install Playwright browsers

After the first restore/build, run Playwright's install script:

```bash
pwsh PlaywrightNUnitTests/bin/Debug/net8.0/playwright.ps1 install
```

> If you build in Release mode, update the path accordingly (`bin/Release/net8.0`).

## Run tests

```bash
dotnet test PlaywrightNUnitTests/PlaywrightNUnitTests.csproj
```

## Notes

- The sample test opens `https://playwright.dev` and validates the page title contains `Playwright`.
- The browser runs in headless mode by default.
