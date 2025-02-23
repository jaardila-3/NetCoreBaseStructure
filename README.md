# NetCoreBaseStructure

A foundational structure for building .NET Core projects, designed with a scalable five-layer architecture to ensure maintainability and best practices. This repository provides a starting point for .NET Core applications using ASP.NET Core MVC, Entity Framework Core with Oracle Database, and modern design patterns like the Generic Repository and Unit of Work. It also incorporates DotNetEnv for environment variable management, making it ideal for development environments.

## Overview
This template includes:

## Five-Layer Architecture:

- **Web Layer (WebApp.Web)**: Presentation layer with ASP.NET Core MVC, areas, controllers, views, and models.
- **Business Layer (WebApp.Business)**: Business logic layer with services for core application rules.
- **Data Layer (WebApp.Data)**: Data access layer with Entity Framework Core, generic repositories, and Unit of Work for Oracle Database.
- **Common Layer (WebApp.Common)**: Shared utilities, constants, extensions, helpers, and models (e.g., DTOs).
- **Services Layer (WebApp.Services)**: Layer for external service integrations with interfaces and implementations.

### Design Patterns:
- **Generic Repository Pattern**: Abstract data access with reusable `IRepository<T>` and concrete `Repository<T>` implementations.
- **Unit of Work Pattern**: Manages transactions and coordinates multiple repositories via `IUnitOfWork`.

### Technologies:
- ASP.NET Core 8.0 (MVC).
- Entity Framework Core 8.0 with Oracle support via Oracle.EntityFrameworkCore.
- Oracle Database as the data store.
- DotNetEnv for managing environment variables (e.g., connection strings).

### Best Practices:
- Dependency Injection for loose coupling.
- Unit and Integration testing support.
- Clean folder structure and naming conventions.
- Remember to remove HTTP headers that may expose sensitive information about your ASP.NET Core 8 application, such as `Server` and `X-Powered-By`. 

### Prerequisites:
Before cloning and running this project, ensure you have the following installed:

- .NET SDK 8.0 or later.
- Oracle Database (e.g., Oracle Express Edition 21c or higher).
- Oracle Managed Data Access Core for .NET (installed via NuGet).
- Graphviz (optional, for rendering PlantUML diagrams).
- A code editor like Visual Studio, Visual Studio Code, or Rider.

Perfect for developers seeking a solid starting point for .NET Core applications, with built-in support for unit and integration testing, documentation, and a professional folder structure. Includes a `README.md`, `.gitignore`, and ready-to-use setup.

**Getting Started**:
1. Clone this repository: `git clone https://github.com/jaardila-3/NetCoreBaseStructure.git`
2. Open the solution in Visual Studio or Rider.
3. Run `dotnet restore`.
4. Install the EF Core tools globally (if not already installed): `dotnet tool install --global dotnet-ef`
5. Install Oracle-specific packages in WebApp.Data: `Oracle.EntityFrameworkCore` and `Oracle.ManagedDataAccess.Core`
6. Create a .env file in the root of the project, based in `example.env`.
7. Run Migrations for Oracle.
8. Build and run the project to explore the structure.

Feel free to contribute, `fork`, or adapt this structure for your own .NET Core projects!

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Architecture Diagram
For a visual representation of the five-layer architecture, refer to the PlantUML diagram in docs/architecture.puml. Use a PlantUML-compatible tool (e.g., VS Code with the PlantUML extension) to render it.