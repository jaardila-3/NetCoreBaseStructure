# NetCoreBaseStructure - Account Management Branch

This branch (`feature/account-management`) extends the foundational structure for building .NET Core projects by implementing a custom user management system. It includes authentication, authorization, and role management using a scalable five-layer architecture, ASP.NET Core MVC with areas, Entity Framework Core with Oracle Database in a Code First approach, and modern design patterns like the Generic Repository and Unit of Work.

## Overview
This branch introduces:
- Custom user management with tables `Users`, `Roles`, and `UserRoles`.
- Authentication and authorization using cookie-based authentication configured in `Program.cs`.
- Account management functionality in the `Account` area, including login, registration, and logout.

## Five-Layer Architecture:

- **Web Layer (WebApp.Web)**: Presentation layer with ASP.NET Core MVC, areas, controllers, views, and models.
- **Business Layer (WebApp.Business)**: Business logic layer with services for core application rules.
- **Data Layer (WebApp.Data)**: Data access layer with Entity Framework Core, generic repositories, and Unit of Work for Oracle Database.
- **Common Layer (WebApp.Common)**: Shared utilities, constants, extensions, helpers, and models (e.g., DTOs).
- **Services Layer (WebApp.Services)**: Layer for external service integrations with interfaces and implementations.

### Design Patterns:
- **Generic Repository Pattern**: Abstract data access with reusable `IRepository<T>` and concrete `Repository<T>` implementations.
- **Unit of Work Pattern**: Manages transactions and coordinates multiple repositories via `IUnitOfWork`.

### Technologies and Dependencies:
- **Framework**: ASP.NET Core 8.0 with cookie-based authentication.
- **Data Access**: Entity Framework Core 8.0 with `Oracle.EntityFrameworkCore` for Oracle Database integration, using a Code First workflow for database schema creation.
- **Database**: Oracle Database (e.g., Oracle Express Edition 21c or higher), requiring `Oracle.ManagedDataAccess.Core`.
- **Environment Management**: `DotNetEnv` for loading environment variables (e.g., Oracle connection strings) from a `.env` file in development.
- **Logging**: `Serilog` for structured logging, configurable to write to files or Oracle Database.
- **Validation**: `FluentValidation` for complex business and data validations, integrated into `WebApp.Common`.
- **Localization**: Configured for Spanish (Colombia, "es-CO") culture, handling decimal separators (comma) and date formats.

### Best Practices:
- **Dependency Injection**: Uses ASP.NET Core’s built-in DI for loose coupling across layers.
- **Exception Handling**: Implements a global exception handler using UseExceptionHandler or custom middleware, with error pages (`Error.cshtml`) and session-based error messaging.
- **Logging**: Centralizes error and activity logging with Serilog, configurable for files and Oracle Database storage.
- **Testing**: Supports unit and integration testing in `WebApp.UnitTests` and `WebApp.IntegrationTests`, with mockable repositories and services.
- **Folder Structure**: Maintains a clean, modular structure with dedicated folders for middlewares, validators, exceptions, and models.
- **Security**: Custom password hashing and role-based authorization.
- **Note**: Remember to remove HTTP headers that may expose sensitive information about your ASP.NET Core 8 application, such as `Server` and `X-Powered-By`. 

### Prerequisites:
Before cloning and running this project, ensure you have the following installed:

- .NET SDK 8.0 or later.
- Oracle Database (e.g., Oracle Express Edition 21c or higher).
- Oracle Managed Data Access Core for .NET (installed via NuGet).
- Graphviz (optional, for rendering PlantUML diagrams).
- Mermaid (optional, required only if you want to render architecture diagrams from `architecture.md`).
- A code editor like Visual Studio, Visual Studio Code, or Rider.

Perfect for developers seeking a solid starting point for .NET Core applications, with built-in support for unit and integration testing, documentation, and a professional folder structure. Includes a `README.md`, `.gitignore`, and ready-to-use setup.

**Getting Started**:
1. Clone this repository: `git clone https://github.com/jaardila-3/NetCoreBaseStructure.git`
2. git checkout `feature/account-management`.
3. Open the solution in Visual Studio or Rider.
4. Run `dotnet restore`.
5. Install the EF Core tools globally (if not already installed): `dotnet tool install --global dotnet-ef`
6. Install Oracle-specific packages in WebApp.Data: `Oracle.EntityFrameworkCore` and `Oracle.ManagedDataAccess.Core`
7. Create a .env file in the root of the project, based in `example.env`.
8. Run Migrations for Oracle (from the root of the project, not within individual layers):
- To create a new migration:
     ```bash
     dotnet ef migrations add InitialMigration -s src/WebApp.Web/ -p src/WebApp.Data/
     ```
9. Build and run the project to explore the structure and apply migrations and update the database.

Feel free to contribute, `fork`, or adapt this structure for your own .NET Core projects!

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Architecture Diagram
For a visual representation of the five-layer architecture, refer to the PlantUML diagram in docs/architecture.puml. Use a PlantUML-compatible tool (e.g., VS Code with the PlantUML extension) to render it.