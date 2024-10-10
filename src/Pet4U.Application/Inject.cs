using Microsoft.Extensions.DependencyInjection;
using Pet4U.Application.UseCases.CreateVolunteer;

namespace Pet4U.Application;

public static class Inject
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();  
    return services;
  }
}