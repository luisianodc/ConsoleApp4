/**
using ConsoleApp4;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides the application's entry point and database initialization workflow.
/// </summary>
internal class Program
{
    /// <summary>
    /// Initializes the database and starts the interactive console menu.
    /// </summary>
    /// <param name="args">Command-line arguments supplied to the application.</param>
    private static void Main(string[] args)
    {
        const string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=space_port_ef;" +
            "Trusted_Connection=True;TrustServerCertificate=True;";

        try
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var db = new MyDbContext(options);
            db.Database.Migrate();

            Console.WriteLine("Database migration completed successfully.");
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine("Could not connect to SQL Server or apply the migration.");
            Console.Error.WriteLine(ex.Message);
            Console.Error.WriteLine("Verify that SQL Server Express is running and that the server instance is localhost\\SQLEXPRESS.");
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine("The database configuration or migration is invalid.");
            Console.Error.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("The application could not initialize the database.");
            Console.Error.WriteLine(ex.Message);
        }
    }
}

*/

using ConsoleApp4;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides the application's entry point and interactive console workflow.
/// </summary>
internal class Program
{
    /// <summary>
    /// Initializes the database and starts the interactive console menu.
    /// </summary>
    /// <param name="args">Command-line arguments supplied to the application.</param>
    private static void Main(string[] args)
    {
        const string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=space_port_ef;" +
            "Trusted_Connection=True;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        try
        {
            using (var db = new MyDbContext(options))
            {
                db.Database.Migrate();
            }

            MainMenu(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Application error:");
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Displays the main menu and dispatches the selected operation.
    /// </summary>
    /// <param name="options">The configured Entity Framework Core options.</param>
    private static void MainMenu(
        DbContextOptions<MyDbContext> options)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("==============================");
            Console.WriteLine("       SPACE PORT SYSTEM");
            Console.WriteLine("==============================");
            Console.WriteLine();
            Console.WriteLine("1. Add a Rocket");
            Console.WriteLine("2. Add a Rocket Crew");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRocket(options);
                    break;

                case "2":
                    AddRocketCrew(options);
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    /// <summary>
    /// Collects rocket details from the user and stores a new rocket.
    /// </summary>
    /// <param name="options">The configured Entity Framework Core options.</param>
    private static void AddRocket(
        DbContextOptions<MyDbContext> options)
    {
        Console.Clear();

        Console.WriteLine("========== ADD ROCKET ==========");
        Console.WriteLine();

        Console.Write("Rocket name: ");
        string rocketName = Console.ReadLine()?.Trim();

        Console.Write("Manufacturer: ");
        string manufacturer = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(rocketName) ||
            string.IsNullOrWhiteSpace(manufacturer))
        {
            Console.WriteLine("All fields are required.");
            Pause();
            return;
        }

        using (var db = new MyDbContext(options))
        {
            // Check if rocket already exists
            bool exists = db.Rockets.Any(r =>
                r.RocketName.ToLower() == rocketName.ToLower() &&
                r.Manufacturer.ToLower() == manufacturer.ToLower());

            if (exists)
            {
                Console.WriteLine();
                Console.WriteLine("This rocket already exists!");
                Pause();
                return;
            }

            Rocket rocket = new Rocket
            {
                RocketName = rocketName,
                Manufacturer = manufacturer
            };

            db.Rockets.Add(rocket);

            // Save to SQL Server
            db.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("Rocket saved successfully!");
            Console.WriteLine("Rocket ID: " + rocket.Id);
        }

        Pause();
    }

    /// <summary>
    /// Displays available rockets and stores a new crew assignment.
    /// </summary>
    /// <param name="options">The configured Entity Framework Core options.</param>
    private static void AddRocketCrew(
        DbContextOptions<MyDbContext> options)
    {
        Console.Clear();

        Console.WriteLine("========== ADD ROCKET CREW ==========");
        Console.WriteLine();

        using (var db = new MyDbContext(options))
        {
            var rockets = db.Rockets.ToList();

            if (rockets.Count == 0)
            {
                Console.WriteLine("No rockets found.");
                Console.WriteLine("Please add a rocket first.");
                Pause();
                return;
            }

            // Display available rockets
            Console.WriteLine("AVAILABLE ROCKETS");
            Console.WriteLine("--------------------------------");

            foreach (var rocket in rockets)
            {
                Console.WriteLine(
                    $"ID: {rocket.Id} | " +
                    $"Name: {rocket.RocketName} | " +
                    $"Manufacturer: {rocket.Manufacturer}");
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine();

            Console.Write("Enter Rocket ID: ");

            if (!int.TryParse(Console.ReadLine(), out int rocketId))
            {
                Console.WriteLine("Invalid Rocket ID.");
                Pause();
                return;
            }

            Rocket selectedRocket = db.Rockets
                .FirstOrDefault(r => r.Id == rocketId);

            if (selectedRocket == null)
            {
                Console.WriteLine("Rocket not found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                "Selected Rocket: " + selectedRocket.RocketName);

            Console.WriteLine();

            Console.Write("Crew member name: ");
            string crewName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(crewName))
            {
                Console.WriteLine("Crew name is required.");
                Pause();
                return;
            }

            // Check if crew member already exists
            bool crewExists = db.RocketCrews.Any(c =>
                c.CrewName.ToLower() == crewName.ToLower() &&
                c.RocketId == rocketId);

            if (crewExists)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "This crew member is already registered for this rocket!");

                Pause();
                return;
            }

            RocketCrew crew = new RocketCrew
            {
                CrewName = crewName,
                RocketId = rocketId
            };

            db.RocketCrews.Add(crew);

            // Save to SQL Server
            db.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("Crew member saved successfully!");
            Console.WriteLine("Crew ID: " + crew.Id);
            Console.WriteLine("Crew Name: " + crew.CrewName);
            Console.WriteLine("Rocket: " + selectedRocket.RocketName);
            Console.WriteLine("Rocket ID: " + crew.RocketId);
        }

        Pause();
    }

    /// <summary>
    /// Pauses the console until the user presses a key.
    /// </summary>
    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

