using Pet4U.Domain.Modules;
using Pet4U.Domain;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public class CreateVolunteerHandler
{

  private readonly IVolunteerRepository _volunteerRepository;

  public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
  {
    _volunteerRepository = volunteerRepository;
  }
  public async Task<Guid> HandleAsync
  (
    CreateVolunteerCommand createVolunteerCommand,
    CancellationToken cancellationToken
  )
  {
    var fullName = FullName.Create
    (
      createVolunteerCommand.CreateVolunteerRequest.FirstName,
      createVolunteerCommand.CreateVolunteerRequest.LastName,
      createVolunteerCommand.CreateVolunteerRequest.MiddleName
    );

    var volunteer = Volunteer.Create
    (
      id: VolunteerId.New(),
      fullName: fullName,
      email: createVolunteerCommand.CreateVolunteerRequest.Email,
      description: Description.Create(createVolunteerCommand.CreateVolunteerRequest.Description),
      experience: createVolunteerCommand.CreateVolunteerRequest.Experience,
      phone: Phone.Create(createVolunteerCommand.CreateVolunteerRequest.Phone),
      socialNetworks: createVolunteerCommand.CreateVolunteerRequest.SocialNetworks,
      paymentInfos: createVolunteerCommand.CreateVolunteerRequest.PaymentInfos
    );

     var result = await _volunteerRepository.AddAsync(volunteer);

    return result;
  }
}