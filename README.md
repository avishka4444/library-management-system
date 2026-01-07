# Library Management System

A full-stack library management system built with Vue.js frontend and ASP.NET Core backend.

## 🏗️ Project Structure

```
library-management-system/
├── backend/              # ASP.NET Core 9.0 API
│   ├── tests/           # Unit tests
│   └── ...
├── frontend/            # Vue 3 + TypeScript + Vite
│   └── ...
├── docker-compose.yml   # Docker Compose configuration
└── .github/workflows/   # CI/CD pipelines
```

## 🚀 Quick Start

### Prerequisites

- **.NET 9.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Node.js 20.x** - [Download](https://nodejs.org/)
- **Docker Desktop** (for SQL Server) - [Download](https://www.docker.com/get-started)

### Option 1: Run with Docker Compose (Recommended)

The easiest way to run the entire system:

```bash
# Start all services (SQL Server, Backend API, Frontend)
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down
```

**Services will be available at:**
- Frontend: http://localhost:3000
- Backend API: http://localhost:5150
- Swagger UI: http://localhost:5150/swagger
- SQL Server: localhost:1433

### Option 2: Run Locally (Development)

#### 1. Start SQL Server

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -e "MSSQL_PID=Developer" -p 1433:1433 --name library-sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Wait for SQL Server to be ready (about 30 seconds), then create the database:

```bash
# Create database
docker exec -i library-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "YourStrong@Passw0rd" -C \
  -Q "IF DB_ID('LibraryManagement') IS NULL CREATE DATABASE LibraryManagement;"

# Create tables (run the SQL script from backend/Scripts/CreateTables.sql)
# You can use SQL Server Management Studio, Azure Data Studio, or docker exec
```

#### 2. Start Backend API

```bash
cd backend
dotnet restore
dotnet run
```

The API will be available at:
- http://localhost:5150
- Swagger UI: http://localhost:5150/swagger

#### 3. Start Frontend

Open a new terminal:

```bash
cd frontend
npm install
npm run dev
```

The frontend will be available at:
- http://localhost:5173

## 📚 Detailed Documentation

### Backend API

See [backend/README.md](./backend/README.md) for:
- Detailed setup instructions
- API endpoints documentation
- Database configuration
- Testing guide

### Frontend

See [frontend/README.md](./frontend/README.md) for:
- Development setup
- Build instructions
- Project structure
- Troubleshooting

## 🧪 Testing

### Backend Tests

```bash
cd backend/tests
dotnet test
```

### Frontend Tests

```bash
cd frontend
npm run test  # If test scripts are configured
```

## 🔧 Development

### Running in Development Mode

1. **Terminal 1 - Backend:**
   ```bash
   cd backend
   dotnet watch run
   ```

2. **Terminal 2 - Frontend:**
   ```bash
   cd frontend
   npm run dev
   ```

### Building for Production

**Backend:**
```bash
cd backend
dotnet publish -c Release
```

**Frontend:**
```bash
cd frontend
npm run build
```

## 🐳 Docker

### Build Images

```bash
# Build backend
docker build -t library-api ./backend

# Build frontend
docker build -t library-frontend ./frontend
```

### Run with Docker Compose

```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Stop and remove volumes
docker-compose down -v
```

## 🔄 CI/CD

The project includes GitHub Actions workflows:

- **Frontend CI/CD** (`.github/workflows/frontend.yml`): Builds and tests the Vue.js application
- **Backend CI/CD** (`.github/workflows/backend.yml`): Builds, tests, and validates the .NET API

Workflows run on:
- Push to `main` or `develop` branches
- Pull requests to `main` or `develop` branches
- Only when relevant files change (path-based triggers)

## 🛠️ Technology Stack

### Backend
- ASP.NET Core 9.0
- SQL Server 2022
- Linq2DB
- xUnit (testing)
- Swagger/OpenAPI

### Frontend
- Vue 3 (Composition API)
- TypeScript
- Vite
- Vue Router
- Tailwind CSS

### Infrastructure
- Docker & Docker Compose
- GitHub Actions

## 📝 Environment Variables

### Backend

Configure in `backend/appsettings.json` or `backend/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=LibraryManagement;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"
  }
}
```

### Frontend

Create `frontend/.env`:

```env
VITE_API_BASE_URL=http://localhost:5150/api
```

## 🐛 Troubleshooting

### Database Connection Issues

1. Ensure SQL Server is running:
   ```bash
   docker ps | grep sqlserver
   ```

2. Check connection string in `backend/appsettings.json`

3. Verify database exists:
   ```bash
   docker exec -it library-sqlserver /opt/mssql-tools18/bin/sqlcmd \
     -S localhost -U sa -P "YourStrong@Passw0rd" -C \
     -Q "SELECT name FROM sys.databases"
   ```

### Frontend Can't Connect to API

1. Verify backend is running: http://localhost:5150/swagger
2. Check `VITE_API_BASE_URL` in `frontend/.env`
3. Check CORS configuration in `backend/Program.cs`

### Port Conflicts

- Backend default: `5150` (change in `backend/Properties/launchSettings.json`)
- Frontend default: `5173` (change with `npm run dev -- --port 3000`)
- SQL Server default: `1433`

## 📖 API Documentation

When the backend is running, visit:
- **Swagger UI**: http://localhost:5150/swagger
- **Health Check**: http://localhost:5150/health

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Run tests (`dotnet test` in backend/tests, `npm test` in frontend)
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

## 📄 License

This project is part of the Library Management System.

## 🆘 Support

For issues and questions:
1. Check the troubleshooting section above
2. Review the detailed README files in `backend/` and `frontend/`
3. Check GitHub Issues
4. Review the API documentation at `/swagger` when the backend is running

