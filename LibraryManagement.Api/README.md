# Library Management API

A RESTful API for managing a library system built with ASP.NET Core, SQL Server, and Linq2DB.

## Quick Start

### Prerequisites
- .NET 9.0 SDK
- Docker Desktop (for SQL Server)

### Setup and Run (5 minutes)

1. **Start SQL Server with Docker:**
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
     -e "MSSQL_PID=Developer" -p 1433:1433 --name library-sqlserver \
     -d mcr.microsoft.com/mssql/server:2022-latest
   ```

2. **Wait for SQL Server to be ready (about 30 seconds):**
   ```bash
   docker logs -f library-sqlserver
   # Press Ctrl+C when you see "SQL Server is now ready for client connections"
   ```

3. **Create database and tables:**
   ```bash
   # Option 1: Using SQL Server Management Studio or Azure Data Studio
   # Connect to: localhost,1433 | sa | YourStrong@Passw0rd
   # Run the script: LibraryManagement.Api/Scripts/CreateTables.sql
   
   # Option 2: Using docker exec (if sqlcmd is available)
   docker exec -i library-sqlserver /opt/mssql-tools/bin/sqlcmd \
     -S localhost -U sa -P "YourStrong@Passw0rd" -C \
     -i /path/to/CreateTables.sql
   ```

4. **Navigate to project and restore packages:**
   ```bash
   cd LibraryManagement.Api
   dotnet restore
   ```

5. **Run the API:**
   ```bash
   dotnet run
   ```

6. **Verify it's working:**
   - Open browser: `http://localhost:5150/swagger`
   - Or test health endpoint: `http://localhost:5150/health`

The API is now running! 🎉

## Features

- **Books Management**: Create, read, update, and delete books
- **Authors Management**: Manage author information
- **Members Management**: Handle library members
- **Borrowings Management**: Track book borrowings and returns
- **Swagger/OpenAPI**: Interactive API documentation
- **CORS Support**: Configured for Vue.js frontend integration

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET 9.0 SDK** - [Download .NET SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Docker** (recommended for SQL Server) - [Download Docker](https://www.docker.com/get-started)
  - OR **SQL Server** (LocalDB, Express, or Full version) - [Download SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Visual Studio 2022** or **VS Code** with C# extension (optional, for development)

## Installation

### 1. Verify .NET SDK Installation

Check if .NET 9.0 SDK is installed:

```bash
dotnet --version
```

You should see version `9.0.x` or higher. If not, install it from the [.NET download page](https://dotnet.microsoft.com/download/dotnet/9.0).

### 2. Navigate to the project directory

```bash
cd LibraryManagement.Api
```

### 3. Restore NuGet Packages

```bash
dotnet restore
```

This will download all required NuGet packages defined in `LibraryManagement.Api.csproj`.

## Setup Instructions

### 1. Database Setup with Docker (Recommended)

1. **Start SQL Server in Docker:**
   ```bash
   docker pull mcr.microsoft.com/mssql/server:2022-latest
   
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
     -p 1433:1433 --name sqlserver \
     -d mcr.microsoft.com/mssql/server:2022-latest
   ```

2. **Create the database:**
   ```bash
   docker run --rm --platform linux/amd64 --network container:sqlserver \
     mcr.microsoft.com/mssql-tools \
     /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" \
     -Q "IF DB_ID('LibraryManagement') IS NULL CREATE DATABASE LibraryManagement;"
   ```

3. **Create the tables:**
   ```bash
   docker run --rm --platform linux/amd64 --network container:sqlserver \
     -v "$(pwd)/Scripts:/scripts" \
     mcr.microsoft.com/mssql-tools \
     /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" \
     -i /scripts/CreateTables.sql
   ```

### 2. Database Setup (Traditional SQL Server)

1. Open SQL Server Management Studio (SSMS) or use `sqlcmd`
2. Run the SQL script located at `Scripts/CreateDatabase.sql` to create the database and tables
3. Alternatively, you can run:
   ```bash
   sqlcmd -S localhost -i Scripts/CreateDatabase.sql
   ```

### 3. Connection String Configuration

The connection string is already configured for Docker SQL Server in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=LibraryManagement;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"
  }
}
```

**Important**: If you're using a different SQL Server setup, update the connection string in `appsettings.json`:

**For Windows Authentication (if using local SQL Server):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LibraryManagement;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

**For SQL Server Authentication:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=LibraryManagement;User Id=your_username;Password=your_password;TrustServerCertificate=true;"
  }
}
```

**For Development Environment** (optional override):
You can also create or modify `appsettings.Development.json` to override settings for development:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your development connection string here"
  }
}
```

### 4. Run the Application

**From the project root directory (`LibraryManagement.Api`):**

```bash
dotnet run
```

**Or build and run separately:**
```bash
dotnet build
dotnet run --no-build
```

**The API will be available at:**
- **HTTP**: `http://localhost:5150`
- **Swagger UI**: `http://localhost:5150/swagger`
- **Health Check**: `http://localhost:5150/health`

**Default ports** (can be configured in `Properties/launchSettings.json`):
- HTTP: `http://localhost:5150`
- HTTPS: `https://localhost:5151` (if configured)

**Verify the API is running:**
1. Open `http://localhost:5150/swagger` in your browser
2. You should see the Swagger UI with all available endpoints
3. Try the `GET /api/Books` endpoint to test the connection

**Note**: Make sure SQL Server is running before starting the API, otherwise you'll get connection errors.

### Stop the Application

Press `Ctrl+C` in the terminal where the API is running.

### Restart the Application

```bash
# Stop with Ctrl+C, then:
dotnet run
```

Or if you made code changes:
```bash
dotnet build
dotnet run
```

## API Endpoints

### Books
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Create a new book
- `PUT /api/books/{id}` - Update a book
- `DELETE /api/books/{id}` - Delete a book

### Authors
- `GET /api/authors` - Get all authors
- `GET /api/authors/{id}` - Get author by ID
- `POST /api/authors` - Create a new author
- `PUT /api/authors/{id}` - Update an author
- `DELETE /api/authors/{id}` - Delete an author

### Members
- `GET /api/members` - Get all members
- `GET /api/members/{id}` - Get member by ID
- `POST /api/members` - Create a new member
- `PUT /api/members/{id}` - Update a member
- `DELETE /api/members/{id}` - Delete a member

### Borrowings
- `GET /api/borrowings` - Get all borrowings
- `GET /api/borrowings/{id}` - Get borrowing by ID
- `GET /api/borrowings/member/{memberId}` - Get borrowings by member
- `POST /api/borrowings` - Create a new borrowing
- `POST /api/borrowings/return` - Return a book
- `DELETE /api/borrowings/{id}` - Delete a borrowing

## Example Requests

### Create an Author
```json
POST /api/authors
{
  "firstName": "J.K.",
  "lastName": "Rowling",
  "dateOfBirth": "1965-07-31",
  "biography": "British author, best known for the Harry Potter series"
}
```

### Create a Book
```json
POST /api/books
{
  "title": "Harry Potter and the Philosopher's Stone",
  "isbn": "978-0747532699",
  "authorId": 1,
  "publishedDate": "1997-06-26",
  "totalCopies": 5
}
```

### Create a Member
```json
POST /api/members
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "+1234567890",
  "address": "123 Main St, City, State"
}
```

### Borrow a Book
```json
POST /api/borrowings
{
  "bookId": 1,
  "memberId": 1,
  "dueDate": "2024-12-31T00:00:00Z"
}
```

### Return a Book
```json
POST /api/borrowings/return
{
  "borrowingId": 1
}
```

## Testing

The project includes comprehensive unit tests using xUnit, Moq, and FluentAssertions.

### Running Tests

```bash
# Navigate to test project
cd LibraryManagement.Api.Tests

# Run all tests
dotnet test

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run with detailed output
dotnet test --verbosity normal
```

### Test Coverage

- **Controller Tests**: Full coverage of API endpoints including success, error, and validation scenarios
- **Service Tests**: Business logic and data access patterns
- **Code Coverage**: Automatically collected and reported in CI/CD pipeline

See [LibraryManagement.Api.Tests/README.md](../LibraryManagement.Api.Tests/README.md) for more details.

## CI/CD

The project includes GitHub Actions workflows for continuous integration and deployment:

- **CI Pipeline**: Runs tests, builds, and validates Docker images on every push/PR
- **Docker Publishing**: Automatically builds and publishes Docker images to GitHub Container Registry

See [.github/workflows/README.md](../.github/workflows/README.md) for workflow details.

## Technology Stack

- **ASP.NET Core 9.0** - Web framework
- **SQL Server** - Database
- **Linq2DB** - Lightweight ORM for data access
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework
- **Docker** - Containerization

## Project Structure

```
LibraryManagement.Api/
├── Controllers/          # API controllers
├── Data/                # Database context
├── DTOs/                # Data transfer objects
├── Models/              # Entity models
├── Scripts/             # SQL scripts
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration
```

## Troubleshooting

### Database Connection Issues

**Symptoms**: 500 errors, "Could not open a connection to SQL Server"

**Solutions:**

1. **Check if SQL Server container is running:**
   ```bash
   docker ps
   # Should show library-sqlserver or sqlserver container with status "Up"
   ```

2. **Start SQL Server if it's stopped:**
   ```bash
   docker start library-sqlserver
   # Or if using different name:
   docker start sqlserver
   ```

3. **Wait for SQL Server to be ready:**
   ```bash
   docker logs library-sqlserver
   # Wait until you see "SQL Server is now ready for client connections"
   ```

4. **Verify database and tables exist:**
   - Connect using SQL Server Management Studio or Azure Data Studio
   - Server: `localhost,1433`
   - Username: `sa`
   - Password: `YourStrong@Passw0rd`
   - Check if `LibraryManagement` database exists
   - Check if tables (Books, Authors, Members, Borrowings) exist
   - If not, run `Scripts/CreateTables.sql`

5. **Check connection string in `appsettings.json`:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=LibraryManagement;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"
     }
   }
   ```

6. **Restart the API after fixing database issues:**
   ```bash
   # Stop with Ctrl+C, then:
   dotnet run
   ```

### Port Already in Use

If the default ports are already in use:

1. Check `Properties/launchSettings.json` for port configuration
2. Modify the `applicationUrl` in `launchSettings.json`:
   ```json
   {
     "applicationUrl": "http://localhost:5150;https://localhost:5151"
   }
   ```

### Build Errors

If you encounter build errors:

1. **Restore packages**: `dotnet restore`
2. **Clean build**: `dotnet clean && dotnet build`
3. **Check .NET version**: Ensure you have .NET 9.0 SDK installed

### CORS Issues

If the frontend cannot connect:

1. Verify CORS is enabled in `Program.cs`
2. Check that the frontend URL is included in the allowed origins
3. Default allowed origins: `http://localhost:5173`, `http://localhost:3000`, `http://localhost:8080`

## Development Tips

- Use Swagger UI (available at root in development) to test API endpoints
- Check the console output for detailed error messages
- Use `appsettings.Development.json` for development-specific overrides
- The API uses Linq2DB for database operations, which provides SQL-focused data access using LINQ

## Notes

- The API uses Linq2DB for database operations, which provides SQL-focused data access using LINQ
- CORS is configured to allow requests from common Vue.js development ports (5173, 3000, 8080)
- All timestamps are stored in UTC
- The API automatically manages book availability when books are borrowed or returned
- Swagger UI is available at the root URL in development mode for easy API exploration

