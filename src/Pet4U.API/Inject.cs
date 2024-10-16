
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pet4U.API.Validation;
using Pet4U.Application.UseCases.CreateVolunteer;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Pet4U.Application;

public static class Inject
{
  public static IServiceCollection AddValidation(this IServiceCollection services)
  {
    services.AddFluentValidationAutoValidation(configuration => {
      
        configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
    });
    return services;
  }
}