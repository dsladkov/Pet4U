using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
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

    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoHandler(IVolunteersRepository volunteerRepository, IUnitOfWork unitOfWork,ILogger<UpdateMainInfoHandler> logger)
      {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
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

    var volunteerUpdated = _volunteerRepository.Add(volunteerResult.Value,cancellationToken);
    await _unitOfWork.SaveChanges(cancellationToken);

    _logger.LogInformation("Volunteer {firstName} {lastName} with id {id} main info has been updated with description {description} and phone {phone}", fullName.FirstName, fullName.LastName,volunteerResult.Value.Id, volunteerResult.Value.Description, volunteerResult.Value.Phone);
    return volunteerUpdated;
  }
}