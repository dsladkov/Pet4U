using Pet4U.Domain.Modules;
using Pet4U.Domain;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public class CreateVolunteerHandler : ICreateVolunteerHandler
{

  private readonly IVolunteersRepository _volunteerRepository;

  public CreateVolunteerHandler(IVolunteersRepository volunteerRepository)
  {
    _volunteerRepository = volunteerRepository;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    CreateVolunteerCommand command,
    CancellationToken cancellationToken
  )
  {
    var fullName = FullName.Create
    (
      command.FirstName,
      command.LastName,
      command.MiddleName
    );

    if (fullName.IsFailure)
      return fullName.Error!;

    var description = Description.Create(command.Description);
    if (description.IsFailure)
      return description.Error!;

    var phoneResult = Phone.Create(command.Phone);
    if (phoneResult.IsFailure)
      return phoneResult.Error!;

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

    return result;
  }
}