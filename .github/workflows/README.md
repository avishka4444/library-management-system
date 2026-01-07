# GitHub Actions Workflows

This directory contains CI/CD workflows for the Library Management System.

## Workflows

### `ci.yml` - Continuous Integration

Runs on every push and pull request to main/develop branches.

**Jobs:**
- **backend-test**: Builds and tests the .NET API, generates code coverage
- **frontend-build**: Builds the Vue.js frontend
- **docker-build**: Builds Docker images for both services
- **integration-test**: Runs integration tests with SQL Server
- **security-scan**: Scans for security vulnerabilities using Trivy

**Features:**
- Code coverage collection and reporting
- Docker image caching for faster builds
- Artifact uploads for build outputs
- Security scanning

### `docker-publish.yml` - Docker Image Publishing

Runs on pushes to main branch and version tags.

**Features:**
- Builds and pushes Docker images to GitHub Container Registry
- Automatic tagging based on branch, PR, version, and commit SHA
- Image caching for faster builds

## Usage

### Running Tests Locally

```bash
# Backend tests
cd LibraryManagement.Api.Tests
dotnet test

# Frontend build
cd library-management
npm run build
```

### Viewing Workflow Results

1. Go to the "Actions" tab in your GitHub repository
2. Click on a workflow run to see detailed logs
3. View test results, coverage reports, and build artifacts

### Manual Workflow Trigger

The `docker-publish.yml` workflow can be manually triggered:
1. Go to Actions tab
2. Select "Docker Publish"
3. Click "Run workflow"

## Secrets and Variables

No secrets are required for basic CI/CD. For publishing to registries:

- `GITHUB_TOKEN`: Automatically provided by GitHub Actions
- For other registries (Docker Hub, Azure Container Registry), add secrets in repository settings

## Code Coverage

Code coverage is automatically collected and uploaded to Codecov. View coverage reports:
- In the Actions workflow summary
- On Codecov dashboard (if configured)

## Docker Images

Docker images are built and can be published to:
- GitHub Container Registry (ghcr.io) - default
- Other registries (configure in workflow)

Image tags follow semantic versioning and include:
- Branch names
- Version tags (v1.0.0)
- Commit SHAs
- Latest (for main branch)

