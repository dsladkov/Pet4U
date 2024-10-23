using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;
using Pet4U.Infrastructure.Interceptors;

namespace Pet4U.Infrastructure;

public sealed class ApplicationDbContext
(
  IConfiguration configuration//, 
  //SoftDeleteInterceptor softDeleteInterceptor
  ) : DbContext
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
      optionsBuilder.UseNpgsql(configuration.GetConnectionString( DATABASE))
        .UseSnakeCaseNamingConvention()
        .UseLoggerFactory( CreatedLoggerFactory() )
        .EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);

      //optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    private ILoggerFactory? CreatedLoggerFactory() =>
        LoggerFactory.Create( builder => { builder.AddConsole();});
}