# Blog API

A full-featured blog API built with .NET 8, Entity Framework Core, and JWT authentication.

## Features

- **Authentication & Authorization**: JWT-based auth with ASP.NET Identity
- **Blog Management**: Full CRUD operations for blog posts with categories and tags
- **Comments System**: Nested comments on blog posts
- **Category & Tag Management**: Organize content with categories and tags
- **Pagination**: Efficient data retrieval with pagination support
- **AutoMapper Integration**: Clean entity-to-DTO mapping
- **Swagger Documentation**: Interactive API documentation

## Architecture

- **Clean Architecture** with separation of concerns
- **Repository Pattern** for data access abstraction
- **Service Layer** for business logic
- **DTO Pattern** for data transfer
- **Entity Framework Core** with SQL Server

## Project Structure

```
BlogAPI/
├── BlogAPI.Core/           # Entities, DTOs, Exceptions, Mapping
├── BlogAPI.Infrastructure/ # Data access, Identity, Security
├── BlogAPI.Services/       # Business logic services
├── BlogAPI.Web/           # API controllers and configuration
└── BlogAPI.Tests/         # Unit tests
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB for development)

### Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BlogAPI
   ```

2. **Configure connection string**
   Update `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlogApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     },
     "TokenKey": "your-super-secret-key-here"
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update --project BlogAPI.Infrastructure --startup-project BlogAPI.Web
   ```

4. **Build and run**
   ```bash
   dotnet build
   dotnet run --project BlogAPI.Web
   ```

5. **Access the API**
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

### Running Tests

```bash
dotnet test
```

## API Endpoints

### Authentication
- `POST /api/account/register` - Register new user
- `POST /api/account/login` - Login user

### Blog Posts
- `GET /api/blogposts` - Get paginated blog posts
- `GET /api/blogposts/{id}` - Get specific blog post
- `POST /api/blogposts` - Create new blog post (auth required)
- `PUT /api/blogposts/{id}` - Update blog post (auth required)
- `DELETE /api/blogposts/{id}` - Delete blog post (auth required)

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get specific category
- `POST /api/categories` - Create category (auth required)
- `PUT /api/categories/{id}` - Update category (auth required)
- `DELETE /api/categories/{id}` - Delete category (auth required)

### Tags
- `GET /api/tags` - Get all tags
- `GET /api/tags/{id}` - Get specific tag
- `POST /api/tags` - Create tag (auth required)
- `PUT /api/tags/{id}` - Update tag (auth required)
- `DELETE /api/tags/{id}` - Delete tag (auth required)

## Configuration

Key configuration settings in `appsettings.json`:

- **ConnectionStrings:DefaultConnection** - Database connection string
- **TokenKey** - JWT signing key (keep secret in production)

## Technologies Used

- **.NET 8** - Web API framework
- **Entity Framework Core** - ORM for data access
- **SQL Server** - Database
- **AutoMapper** - Object mapping
- **JWT Bearer Authentication** - Security
- **Swagger/OpenAPI** - API documentation
- **MSTest** - Unit testing

## Development

### Adding New Features

1. **Entities**: Add to `BlogAPI.Core/Entities/`
2. **DTOs**: Add to `BlogAPI.Core/Entities/DTOs/`
3. **Services**: Add interface to `BlogAPI.Services/` and implementation
4. **Controllers**: Add to `BlogAPI.Web/Controllers/`
5. **Mapping**: Update `BlogAPI.Core/Mapping/MappingProfile.cs`
6. **Tests**: Add to `BlogAPI.Tests/`

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName --project BlogAPI.Infrastructure --startup-project BlogAPI.Web

# Update database
dotnet ef database update --project BlogAPI.Infrastructure --startup-project BlogAPI.Web
```

## License

This project is licensed under the MIT License.
