
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pet4U.Application.UseCases.AddBreeds;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.AddPetsMediaFiles;
using Pet4U.Application.UseCases.CreatePet;
using Pet4U.Application.UseCases.CreateSpecies;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.UseCases.DeleteVolunteer;
using Pet4U.Application.UseCases.GetUrlPetPhoto;
using Pet4U.Application.UseCases.RemovePetPhotoFile;
using Pet4U.Application.UseCases.UpdateMainInfo;
using Pet4U.Application.UseCases.UpdatePaymentInfos;
using Pet4U.Application.UseCases.UpdateSocialNetworks;

namespace Pet4U.Application;

public static class Inject
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();
    services.AddScoped<IUpdateMainInfoHandler, UpdateMainInfoHandler>();
    services.AddScoped<IDeleteVolunteerHandler, DeleteVolunteerHandler>();
    services.AddScoped<IUpdateSocialNetworks, UpdateSocialNetworksHandler>();
    services.AddScoped<IUpdatePaymentInfosHandler,UpdatePaymentInfosHandler>();
    services.AddScoped<IAddPetPhotoHandler, AddPetPhotoHandler>();
    services.AddScoped<IGetUrlPetPhotoHandler, GetUrlPetPhotoHandler>();
    services.AddScoped<IRemovePetPhotoHandler, RemovePetPhotohandler>();
    services.AddScoped<IUploadMediaHandler, UploadMediaHandler>();
    services.AddScoped<ICreateSpeciesHandler, CreateSpeciesHandler>();
    services.AddScoped<IAddBreedsHandler, AddBreedsHandler>();
    services.AddScoped<ICreatePetHandler, CreatePetHandler>();
    services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
     
    return services;
  }
}