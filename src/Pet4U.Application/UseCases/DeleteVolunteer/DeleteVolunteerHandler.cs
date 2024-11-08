using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
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
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerHandler(IVolunteersRepository volunteerRepository, IUnitOfWork unitOfWork ,ILogger<DeleteVolunteerHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
    _unitOfWork = unitOfWork;
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
    //var volunteerDeleted = _volunteerRepository.Add(volunteerResult.Value,cancellationToken); //Instead of Delete it will be marked as deleted at entity bool prop
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    
    _logger.LogInformation("Volunteer {firstName} {lastName} with id {0} is deleted", volunteerResult.Value.FullName.FirstName, volunteerResult.Value.FullName.MiddleName, volunteerResult.Value.Id);
    return volunteerResult.Value;
  }
}