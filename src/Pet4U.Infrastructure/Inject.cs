
using Pet4U.Application.UseCases.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;
using Pet4U.Infrastructure.Repositories;

namespace Pet4U.Infrastructure;

public static class Inject
{
  public static IServiceCollection AddInfraStructure(this IServiceCollection services)
  {
    services.AddScoped<ApplicationDbContext>();
    services.AddScoped<IVolunteersRepository, VolunteersRepository>();

    return services;
  }
}