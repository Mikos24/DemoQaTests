# SpecFlow Selenium Tests - DemoQA Login

This project contains automated tests using SpecFlow, Selenium WebDriver, and NUnit for testing DemoQA login functionality.

## Project Structure

- `Features/` - Contains Gherkin feature files
- `StepDefinitions/` - Contains step definition classes
- `Hooks/` - Contains setup and teardown hooks
- `Pages/` - Contains page object model classes
- `Utilities/` - Contains helper classes

## Running Tests

### Run all tests
```powershell
dotnet test
```

### Run specific feature
```powershell
dotnet test --filter "FullyQualifiedName~Login"
```

### Run tests with detailed output
```powershell
dotnet test --logger "console;verbosity=detailed"
```

## Features

### Login.feature
Tests DemoQA login functionality including:
- Navigating to the login page
- Entering valid credentials
- Verifying successful login and profile page display

## Test Credentials

The test uses default credentials:
- Username: `testuser`
- Password: `Password123!`

Note: These are placeholder credentials. Update the LoginSteps.cs file with actual valid DemoQA credentials.

## Configuration

- `specflow.json` - SpecFlow configuration
- `NLog.config` - Logging configuration
- `SeleniumTests.csproj` - Project configuration with NuGet packages

## Dependencies

- SpecFlow 3.9.74
- SpecFlow.NUnit 3.9.74
- Selenium WebDriver 4.30.0
- NUnit 3.13.3
- NLog 5.4.0
- WebDriverManager 2.17.5
