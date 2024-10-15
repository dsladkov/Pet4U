using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Infrastructure;

public class ApplicationDbContext() : DbContext //IConfiguration configuration
{
  private const string DATABASE = "Database";

  public DbSet<Volunteer> Volunteers => Set<Volunteer>();
  public DbSet<Species> Species => Set<Species>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .HasDefaultSchema("core")
      .ApplyConfigurationsFromAssembly( typeof(ApplicationDbContext).Assembly );
  }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=pet4u;User Id=postgres;Password=postgres;")
        .UseSnakeCaseNamingConvention()
        .UseLoggerFactory( CreatedLoggerFactory() )
        .EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    private ILoggerFactory? CreatedLoggerFactory() =>
        LoggerFactory.Create( builder => { builder.AddConsole();});
}