using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pet4U.Domain;
using Pet4U.Domain.Modules;

namespace Pet4U.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
  private const string DATABASE = "Database";

  public DbSet<Volunteer> Volunteers => Set<Volunteer>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .HasDefaultSchema("core")
      .ApplyConfigurationsFromAssembly( typeof(ApplicationDbContext).Assembly );
  }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(configuration.GetConnectionString( DATABASE))
        .UseSnakeCaseNamingConvention()
        .UseLoggerFactory( CreatedLoggerFactory() );
        base.OnConfiguring(optionsBuilder);
    }

    private ILoggerFactory? CreatedLoggerFactory() =>
        LoggerFactory.Create( builder => { builder.AddConsole();});
}