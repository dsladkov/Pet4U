using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;

namespace Pet4U.Application.UseCases.DeleteVolunteer;

/// <summary>
/// Soft delete
/// </summary>
public class DeleteVolunteerHandler : IDeleteVolunteerHandler
{

  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(IVolunteersRepository volunteerRepository, ILogger<DeleteVolunteerHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    DeleteVolunteerCommand command,
    CancellationToken cancellationToken
  )
  {
    var volunteerResult = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.Id), cancellationToken);
    if(volunteerResult.IsFailure)
      return volunteerResult.Error;

    volunteerResult.Value.Delete(); //If delete interceptor is disabled
//  volunteerResult.Value.SetInactive();
    var volunteerDeleted = await _volunteerRepository.Delete(volunteerResult.Value,cancellationToken); //Instead of Delete it will be marked as deleted at entity bool prop
    
    _logger.LogInformation("Volunteer with id {0} is deleted", volunteerResult.Value.Id);
    return volunteerDeleted.Value;
  }
}