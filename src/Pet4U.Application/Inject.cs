
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.UseCases.DeleteVolunteer;
using Pet4U.Application.UseCases.UpdateMainInfo;

namespace Pet4U.Application;

public static class Inject
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();
    services.AddScoped<IUpdateMainInfoHandler, UpdateMainInfoHandler>();
    services.AddScoped<IDeleteVolunteerHandler, DeleteVolunteerHandler>();
    services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
     
    return services;
  }
}