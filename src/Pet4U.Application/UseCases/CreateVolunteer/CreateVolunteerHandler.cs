using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public class CreateVolunteerHandler : ICreateVolunteerHandler
{

  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVolunteerHandler(IVolunteersRepository volunteerRepository, IUnitOfWork unitOfWork ,ILogger<CreateVolunteerHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
        _unitOfWork = unitOfWork;
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

    var paymentInfos = command?.PaymentInfos?.Select(p => PaymentInfo.Create(p.title, p.description).Value).ToImmutableArray();
    var socialNetwork = command?.SocialNetworks?.Select(s => SocialNetwork.Create(s.title, s.link).Value).ToImmutableArray();

    var volunteer = Volunteer.Create
    (
      id: VolunteerId.New(),
      fullName: fullName.Value,
      email: command.Email,
      description: description.Value,
      experience: command.Experience,
      phone: phoneResult.Value,
      socialNetworks: socialNetwork, //command.SocialNetworks,
      paymentInfos: paymentInfos//command.PaymentInfos
    );

    if (volunteer.IsFailure)
      return volunteer.Error!;

    var result = _volunteerRepository.Add(volunteer.Value, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    _logger.LogInformation("Volunteer {FullName} {LastName} with Id: {Id} has been created", volunteer.Value.FullName.FirstName, volunteer.Value.FullName.LastName ,volunteer.Value.Id);
    return result;
  }
}