using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp4;

/// <summary>
/// Creates <see cref="MyDbContext"/> instances for Entity Framework Core design-time operations.
/// </summary>
public sealed class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    /// <summary>
    /// Creates a configured context for EF Core migrations and other design-time operations.
    /// </summary>
    /// <param name="args">Command-line arguments supplied by the EF Core tools.</param>
    /// <returns>A configured <see cref="MyDbContext"/> instance.</returns>
    public MyDbContext CreateDbContext(string[] args)
    {
        const string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=space_port_ef;" +
            "Trusted_Connection=True;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new MyDbContext(options);
    }
}
