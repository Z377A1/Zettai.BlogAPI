# Docker Compose Setup

This directory contains Docker Compose configurations for running the Blog API with SQL Server.

## Files

- `docker-compose.yml` - Full application stack (API + SQL Server)
- `docker-compose.dev.yml` - SQL Server only for local development

## Quick Start

### Option 1: SQL Server Only (Recommended for Development)

Run SQL Server in Docker and the API locally:

```bash
# Start SQL Server
docker-compose -f docker-compose.dev.yml up -d

# Wait for SQL Server to start (about 30 seconds)
sleep 30

# Run migrations
dotnet ef database update --project BlogAPI.Infrastructure --startup-project BlogAPI.Web

# Run the API locally
dotnet run --project BlogAPI.Web
```

### Option 2: Full Stack in Docker

Run both API and SQL Server in containers:

```bash
# Build and start everything
docker-compose up --build

# Wait for startup, then access:
# - API: http://localhost:5000
# - Swagger: http://localhost:5000/swagger
```

## Configuration

### SQL Server Settings

- **Server**: `localhost,1433`
- **Database**: `BlogApiDb`
- **Username**: `sa`
- **Password**: `BlogApi123!`

### Environment Variables

The API container uses these environment variables:

- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__DefaultConnection` - SQL Server connection
- `TokenKey` - JWT signing key

## Database Setup

After starting SQL Server, run migrations:

```bash
# Update connection string in appsettings.Development.json, then:
dotnet ef database update --project BlogAPI.Infrastructure --startup-project BlogAPI.Web
```

## Cleanup

```bash
# Stop and remove containers
docker-compose down

# Remove volumes (deletes database data)
docker-compose down -v
```

## Troubleshooting

### SQL Server Connection Issues

1. Wait 30-60 seconds after starting for SQL Server to initialize
2. Check container logs: `docker-compose logs sqlserver`
3. Verify port 1433 is not in use by another SQL Server instance

### Permission Issues

If you get permission errors:

```bash
# Fix Docker permissions (Linux/Mac)
sudo chown -R $USER:$USER .
```

### Container Build Issues

```bash
# Clean rebuild
docker-compose down
docker-compose build --no-cache
docker-compose up
```
