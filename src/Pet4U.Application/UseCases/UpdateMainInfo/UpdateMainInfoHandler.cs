using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;

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

    var descriptionResult = Description.Create(command.Description).Value;
    // if(descriptionResult.IsFailure)
    //   return descriptionResult.Error;

    var phoneResult = Phone.Create(command.Phone).Value;
    // if(phoneResult.IsFailure)
    //   return phoneResult.Error;

    volunteerResult?.Value?.UpdateMainInfo(descriptionResult, phoneResult);

    var volunteerUpdated = await _volunteerRepository.Save(volunteerResult.Value,cancellationToken);
    // var fullName = FullName.Create
    // (
    //   command.FullNameDto.FirstName,
    //   command.FullNameDto.LastName,
    //   command.FullNameDto.MiddleName
    // );

    // // if (fullName.IsFailure)
    // //   return fullName.Error;

    // var description = Description.Create(command.Description);

    // // if (description.IsFailure)
    // //   return description.Error!;

    // var phoneResult = Phone.Create(command.Phone);

    // // if (phoneResult.IsFailure)
    // //   return phoneResult.Error!;

    // var volunteer = Volunteer.Update
    // (
    //   id: VolunteerId.New(),
    //   fullName: fullName.Value,
    //   email: command.Email,
    //   description: description.Value,
    //   experience: command.Experience,
    //   phone: phoneResult.Value,
    //   socialNetworks: command.SocialNetworks,
    //   paymentInfos: command.PaymentInfos
    // );

    // if (volunteer.IsFailure)
    //   return volunteer.Error!;

    // var result = await _volunteerRepository.AddAsync(volunteer.Value, cancellationToken);
    // _logger.LogInformation("Volunteer Id: {Id} has been created", volunteer.Value.Id);
    // return result;
    _logger.LogInformation("Volunteer with id {0} has been updated with description {1} and phone {2}", volunteerResult.Value.Id, volunteerResult.Value.Description, volunteerResult.Value.Phone);
    return volunteerUpdated;
  }
}