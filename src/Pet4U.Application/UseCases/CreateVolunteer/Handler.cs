using Microsoft.Extensions.Logging;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public class CreateVolunteerHandler : ICreateVolunteerHandler
{

  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(IVolunteersRepository volunteerRepository, ILogger<CreateVolunteerHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    CreateVolunteerCommand command,
    CancellationToken cancellationToken
  )
  {
    var fullName = FullName.Create
    (
      command.FullNameDto.FirstName,
      command.FullNameDto.LastName,
      command.FullNameDto.MiddleName
    );

    // if (fullName.IsFailure)
    //   return fullName.Error;

    var description = Description.Create(command.Description);

    // if (description.IsFailure)
    //   return description.Error!;

    var phoneResult = Phone.Create(command.Phone);

    // if (phoneResult.IsFailure)
    //   return phoneResult.Error!;

    var volunteer = Volunteer.Create
    (
      id: VolunteerId.New(),
      fullName: fullName.Value,
      email: command.Email,
      description: description.Value,
      experience: command.Experience,
      phone: phoneResult.Value,
      socialNetworks: command.SocialNetworks,
      paymentInfos: command.PaymentInfos
    );

    if (volunteer.IsFailure)
      return volunteer.Error!;

    var result = await _volunteerRepository.AddAsync(volunteer.Value, cancellationToken);
    _logger.LogInformation("Volunteer Id: {Id} has been created", volunteer.Value.Id);
    return result;
  }
}