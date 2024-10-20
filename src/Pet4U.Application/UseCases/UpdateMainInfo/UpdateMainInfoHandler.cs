using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdateMainInfo;


public class UpdateMainInfoHandler : IUpdateMainInfoHandler
{
  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(IVolunteersRepository volunteerRepository, ILogger<UpdateMainInfoHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    UpdateMainInfoVolunteerCommand command,
    CancellationToken cancellationToken
  )
  {
    var volunteerResult = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);
    if(volunteerResult.IsFailure)
      return volunteerResult.Error;

     var fullName = FullName.Create
    (
      command.FullNameDto.FirstName,
      command.FullNameDto.LastName,
      command.FullNameDto.MiddleName
    ).Value;

    var descriptionResult = Description.Create(command.Description).Value;

    var phoneResult = Phone.Create(command.Phone).Value;

    volunteerResult?.Value?.UpdateMainInfo(fullName, command.Email, command.Experience, descriptionResult, phoneResult);

    var volunteerUpdated = await _volunteerRepository.Save(volunteerResult.Value,cancellationToken);
    
    _logger.LogInformation("Volunteer with id {0} has been updated with description {1} and phone {2}", volunteerResult.Value.Id, volunteerResult.Value.Description, volunteerResult.Value.Phone);
    return volunteerUpdated;
  }
}