
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreateVolunteer;

  public record SocialNetworkDto
  {
    public string Title { get; init;} = null!;
    public string Link { get; init; } = null!;
  }
  public record UpdateSocialNetworkListDto(IReadOnlyCollection<SocialNetworkDto> SocialNetworkDtos);
  public record UpdateSocialNetworkListRequest(Guid Id, UpdateSocialNetworkListDto Dto)
  {
    public UpdateSocialNetworkListCommand ToCommand() => new(Id, Dto.SocialNetworkDtos );
  }

public record DeleteVolunteerCommand (Guid Id);
public record DeleteVolunteerRequest
(
  Guid Id
)
{
  public DeleteVolunteerCommand ToCommand() => new (Id);
}

public record UpdateMainInfoDto(FullNameDto FullNameDto, string Email,string Description, int Experience ,string Phone);
public record UpdateMainInfoVolunteerRequest
(
  Guid VolunteerId,
  UpdateMainInfoDto Dto
)
{
  public UpdateMainInfoVolunteerCommand ToCommand() => 
  new( 
    VolunteerId, 
    Dto.FullNameDto, 
    Dto.Email, 
    Dto.Description, 
    Dto.Experience, 
    Dto.Phone
    );
}

public record CreateVolunteerRequest
(
  //string FirstName,
  //string LastName,
  //string MiddleName,
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfo>? PaymentInfos,
  IReadOnlyCollection<SocialNetwork>? SocialNetworks
)
{
  public CreateVolunteerCommand ToCommand() => new(FullNameDto, Email, Description, Experience, Phone, PaymentInfos, SocialNetworks); 
  //FirstName, LastName, MiddleName, 
}





public record FullNameDto(string FirstName, string LastName, string MiddleName);

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
  public CreateVolunteerRequestValidator()
  {
    RuleFor(c => c.Email).EmailAddress().WithError(Errors.General.ValueIsInvalid("Email"));//.WithMessage(Errors.General.ValueIsInvalid("email").Serialize());
    RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);
    // RuleFor(c => new {c.FirstName, c.LastName, c.MiddleName }).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    RuleFor(c => c.FullNameDto).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
  }
}

public class UpdateMainInfoVolunteerRequestValidator : AbstractValidator<UpdateMainInfoVolunteerRequest>
{
  public UpdateMainInfoVolunteerRequestValidator()
  {
    RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleFor(r => r.Dto.FullNameDto).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    RuleFor(r => r.Dto.Email).NotEmpty().EmailAddress();
    RuleFor(r => r.Dto.Experience).NotEmpty().WithError(Errors.General.ValueIsRequired("Experience"));
    RuleFor(r => r.Dto.Description).NotEmpty().MustBeValueObject(Description.Create);
    RuleFor(r => r.Dto.Phone).NotEmpty().MustBeValueObject(Phone.Create);
  }
}

public class UpdateSocialNetworkListVolunteerRequestValidator : AbstractValidator<UpdateSocialNetworkListRequest>
{
  public UpdateSocialNetworkListVolunteerRequestValidator()
  {
    RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleForEach(r => r.Dto.SocialNetworkDtos).NotNull();
  }
}


public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
{
  public DeleteVolunteerRequestValidator() => RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
}


public record CreateVolunteerCommand
(
  //string FirstName,
  //string LastName,
  //string MiddleName,
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfo>? PaymentInfos,
  IReadOnlyCollection<SocialNetwork>? SocialNetworks
);

public record UpdateMainInfoVolunteerCommand
( Guid VolunteerId,
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone
);

public record UpdateSocialNetworkListCommand
( Guid VolunteerId,
  IReadOnlyCollection<SocialNetworkDto> SocialNetworkDtos
);