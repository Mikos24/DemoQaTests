# DemoQA Automation Tests

This project wos prepared for Glencore as part of the interview process. It contains automated tests using SpecFlow, Selenium WebDriver, RestSharp, and NUnit for testing DemoQA functionality.

## Project Structure

### UITests/
- `Features/` - Contains Gherkin feature files
- `Steps/` - Contains step definition classes and scenario hooks
- `PageObjects/` - Contains page object model classes
- `Utils/` - Contains helper classes (TestHelpers, WebDriverFactory, LoggingConfig)

### APITests/
- `Models/` - Contains request/response models
- `TestData/` - Contains test data files
- `Utils/` - Contains API helper classes

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
- `DemoQATests.csproj` - Project configuration with NuGet packages

### Logging

The project uses NLog for logging with a centralized configuration approach:
- `LoggingConfig` utility class handles automatic NLog setup
- Logs are written to both console and `LogFile.txt` in the output directory
- All test classes inherit logging capability through `BasePage` or use `LoggingConfig.GetCurrentClassLogger()`

### Test Architecture

- **ScenarioHooks**: Manages WebDriver lifecycle (setup/teardown) per scenario
- **BasePage**: Base class for all page objects with built-in logging
- **TestHelpers**: Utility methods for common test operations
- **WebDriverFactory**: Creates and configures WebDriver instances

## Dependencies

- SpecFlow 3.9.74
- SpecFlow.NUnit 3.9.74
- Selenium WebDriver 4.30.0
- NUnit 3.13.3
- NLog 5.4.0
- WebDriverManager 2.17.5

## Recent Improvements

### Logging Refactoring
- Centralized logging configuration through `LoggingConfig` utility class
- Automatic NLog initialization with thread-safe setup
- Removed scattered logging setup from test hooks
- Improved separation of concerns between infrastructure and test logic

### Project Organization
- Moved scenario hooks to `Steps/` folder to group all test attribute classes together
- Renamed `WebDriverHooks` to `ScenarioHooks` for better clarity
- Removed empty `Hooks/` folder for cleaner project structure
