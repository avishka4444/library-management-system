# Library Management API Tests

This project contains unit tests for the Library Management API using xUnit, Moq, and FluentAssertions.

## Quick Start

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022, VS Code, or Rider (optional, for running tests in IDE)

### Setup and Run

1. **Navigate to the test project:**
   ```bash
   cd backend/tests
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Run all tests:**
   ```bash
   dotnet test
   ```

4. **Verify tests pass:**
   You should see output like:
   ```
   Test Run Successful.
   Total tests: 15
        Passed: 15
   ```

That's it! All tests should pass. ✅

### Stop Tests

If tests are running and you need to stop them:
- Press `Ctrl+C` in the terminal

### Re-run Tests After Code Changes

```bash
# Just run again - no need to restore unless dependencies changed
dotnet test
```

## Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022, VS Code, or Rider (optional, for running tests in IDE)

## Running Tests

### Command Line

**Basic test run:**
```bash
# Navigate to test project directory
cd backend/tests

# Restore packages (first time only)
dotnet restore

# Run all tests
dotnet test
```

**Advanced options:**
```bash
# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~BooksControllerTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~GetBooks_ShouldReturnOkResultWithBooks"

# Run tests and show detailed results
dotnet test --logger "console;verbosity=detailed"
```

**From solution root:**
```bash
# Run tests from the root directory
dotnet test backend/tests/LibraryManagement.Api.Tests.csproj
```

### Visual Studio / Rider

1. Open the solution in your IDE
2. Use the Test Explorer to run tests
3. Right-click on test methods or classes to run/debug

## Test Structure

```
backend/tests/
├── Controllers/
│   └── BooksControllerTests.cs    # Controller unit tests
├── Services/
│   └── BookServiceTests.cs        # Service layer tests
└── README.md
```

## Test Coverage

The tests cover:

- **Controller Tests**: 
  - GET endpoints (success and not found scenarios)
  - POST endpoints (success, validation errors, business rule violations)
  - PUT endpoints (success, not found, validation errors)
  - DELETE endpoints (success, not found, business rule violations)

- **Service Tests**: 
  - Business logic validation
  - Data access patterns
  - Error handling

## Code Coverage

Code coverage reports are generated using Coverlet. To view coverage:

```bash
# Generate coverage report
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML report (requires ReportGenerator)
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html
```

## Adding New Tests

When adding new tests:

1. Follow the existing naming convention: `MethodName_Scenario_ExpectedBehavior`
2. Use FluentAssertions for readable assertions
3. Mock dependencies using Moq
4. Test both success and failure scenarios
5. Include edge cases and boundary conditions

### Example Test Structure

```csharp
[Fact]
public async Task MethodName_WhenCondition_ShouldReturnExpectedResult()
{
    // Arrange
    var mockDependency = new Mock<IDependency>();
    // ... setup mocks

    // Act
    var result = await service.MethodName();

    // Assert
    result.Should().NotBeNull();
    // ... more assertions
}
```

## Continuous Integration

Tests run automatically on:
- Push to main/develop branches
- Pull requests
- See `.github/workflows/ci.yml` for CI configuration

