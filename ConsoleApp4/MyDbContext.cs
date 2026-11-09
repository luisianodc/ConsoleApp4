
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4
{
    /// <summary>
    /// Represents the Entity Framework Core database session for the application.
    /// </summary>
    public class MyDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new database context with the configured provider options.
        /// </summary>
        /// <param name="options">The options used to configure this context.</param>
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the rockets tracked by this context.
        /// </summary>
        public DbSet<Rocket> Rockets { get; set; }

        /// <summary>
        /// Gets or sets the crew members tracked by this context.
        /// </summary>
        public DbSet<RocketCrew> RocketCrews { get; set; }

        /// <summary>
        /// Configures table names, field constraints, and the rocket-to-crew relationship.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the entity model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RocketCrew>()
                .ToTable("rocket_crew");

            modelBuilder.Entity<RocketCrew>()
                .Property(rc => rc.CrewName)
                .HasMaxLength(100)
                .IsUnicode(false);

            modelBuilder.Entity<RocketCrew>()
                .Property(rc => rc.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<RocketCrew>()
                .HasOne(rc => rc.Rocket)
                .WithMany(r => r.RocketCrews)
                .HasForeignKey(rc => rc.RocketId);
        }
    }
}
