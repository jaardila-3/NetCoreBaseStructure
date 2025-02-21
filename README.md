# NetCoreBaseStructure

A foundational structure for building .NET Core projects, designed with a scalable five-layer architecture to ensure maintainability and best practices. This repository provides:

- **Web Layer (WebApp.Web)**: Presentation layer featuring MVC or API controllers, areas, views, and models.
- **Business Layer (WebApp.Business)**: Business logic layer with services for core application rules.
- **Data Layer (WebApp.Data)**: Data access layer including Entity Framework, repositories, and migrations.
- **Common Layer (WebApp.Common)**: Shared utilities, constants, extensions, helpers, and models (e.g., DTOs).
- **Services Layer (WebApp.Services)**: Layer for external service integrations with interfaces and implementations.

Perfect for developers seeking a solid starting point for .NET Core applications, with built-in support for unit and integration testing, documentation, and a professional folder structure. Includes a `README.md`, `.gitignore`, and ready-to-use setup.

**Getting Started**:
1. Clone this repository: `git clone https://github.com/jaardila-3/NetCoreBaseStructure.git`
2. Open the solution in Visual Studio or Rider.
3. Run `dotnet restore`.
4. Build and run the project to explore the structure.

Feel free to contribute, fork, or adapt this structure for your own .NET Core projects!