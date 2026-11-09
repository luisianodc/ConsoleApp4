# ConsoleApp4

ConsoleApp4 is a .NET console application that manages rockets and their crew assignments using Entity Framework Core and SQL Server Express.

## Features

- Creates and applies Entity Framework Core database migrations.
- Adds rockets with a name and manufacturer.
- Prevents duplicate rockets.
- Assigns crew members to existing rockets.
- Prevents duplicate crew assignments for the same rocket.
- Provides an EF Core design-time factory for migration commands.

## Requirements

- .NET 10 SDK
- SQL Server Express running as the `SQLEXPRESS` instance
- Visual Studio Package Manager Console or the `dotnet-ef` command-line tool

## Database configuration

The application currently uses this SQL Server connection:

```text
Server=localhost\SQLEXPRESS;Database=space_port_ef;Trusted_Connection=True;TrustServerCertificate=True;
```

The connection string is defined in `Program.cs` for runtime execution and in `MyDbContextFactory.cs` for Entity Framework Core design-time operations.

Make sure the SQL Server Express service is running before starting the application.

## Build and run

From the solution directory, run:

```powershell
dotnet restore
dotnet build
dotnet run --project ConsoleApp4
```

The application applies pending migrations at startup, then displays the Space Port System menu.

## Entity Framework Core migrations

The project includes an `IDesignTimeDbContextFactory<MyDbContext>` implementation so EF Core can create the context from Package Manager Console.

### Package Manager Console

```powershell
Add-Migration FirstMigration
Update-Database
```

### .NET CLI

```powershell
dotnet ef migrations add FirstMigration --project ConsoleApp4
dotnet ef database update --project ConsoleApp4
```

Create additional migrations after changing the entity model:

```powershell
Add-Migration DescribeYourChange
Update-Database
```

## Project structure

| File | Purpose |
| --- | --- |
| `Program.cs` | Console entry point, menu, and database operations. |
| `MyDbContext.cs` | Entity Framework Core context and model configuration. |
| `MyDbContextFactory.cs` | Creates the context for EF Core design-time commands. |
| `Rocket.cs` | Rocket entity. |
| `RocketCrew.cs` | Rocket crew entity and relationship to a rocket. |
| `Migrations/` | Generated EF Core migration files. |

## Troubleshooting

### Unable to resolve `DbContextOptions<MyDbContext>`

Ensure `MyDbContextFactory.cs` exists and the `Microsoft.EntityFrameworkCore.Design` package is restored. Then rebuild before running `Add-Migration`.

### SQL Server instance failure

Verify that:

1. SQL Server Express is installed.
2. The SQL Server (`SQLEXPRESS`) service is running.
3. The server name is `localhost\SQLEXPRESS`.
4. Windows authentication is available to the current user.

### Migration build failure

Run the following commands to restore packages and identify compile errors:

```powershell
dotnet clean
dotnet restore
dotnet build
```
 # Menu


## License

This project is intended for educational and development use.
