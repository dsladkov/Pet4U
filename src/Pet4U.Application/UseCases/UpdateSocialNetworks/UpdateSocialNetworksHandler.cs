using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
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
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSocialNetworksHandler(IVolunteersRepository volunteerRepository, IUnitOfWork unitOfWork, ILogger<UpdateSocialNetworksHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
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
                        let sn = SocialNetwork.Create(item.title, item.link )
                        select sn.Value;

    //var socialNetworks = command.SocialNetworkDtos.Select(s => SocialNetwork.Create(s.Title, s.Link).Value);
     
    volunteerResult?.Value?.UpdateSocialNetworks(new SocialNetworks(socialNetworks));

    //volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

    var volunteerUpdated = _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
    await _unitOfWork.SaveChanges(cancellationToken);
    
    _logger.LogInformation("SocialNetworks of Volunteer {firstName} {lastName} with id {id} have been updated",
      volunteerResult.Value.FullName.FirstName,
      volunteerResult.Value.FullName.LastName,
      volunteerResult.Value.Id);
      
    return volunteerUpdated;
  }
}