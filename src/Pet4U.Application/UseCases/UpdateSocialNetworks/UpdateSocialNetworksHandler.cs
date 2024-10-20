using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdateSocialNetworks;


public class UpdateSocialNetworksHandler : IUpdateSocialNetworks
{
  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;

    public UpdateSocialNetworksHandler(IVolunteersRepository volunteerRepository, ILogger<UpdateSocialNetworksHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    UpdateSocialNetworkListCommand command,
    CancellationToken cancellationToken
  )
  {
    var volunteerResult = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);
    if(volunteerResult.IsFailure)
      return volunteerResult.Error;

   var socialNetworks =  from item in command.SocialNetworkDtos
                         let sn = SocialNetwork.Create(item.Title, item.Link )
                         select sn.Value;

  //var socialNetworks = command.SocialNetworkDtos.Select(s => SocialNetwork.Create(s.Title, s.Link).Value);
     
   volunteerResult?.Value?.UpdateSocialNetworks(new SocialNetworks(socialNetworks));

    //volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

    var volunteerUpdated = await _volunteerRepository.Save(volunteerResult.Value,cancellationToken);
    
    _logger.LogInformation("SocialNetworks of Volunteer with id {0} have been updated", volunteerResult.Value.Id);
    return volunteerUpdated;
  }
}