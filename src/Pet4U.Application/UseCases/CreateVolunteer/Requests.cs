
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public record UpdateMainInfoDto(string Description, string Phone);
public record UpdateMainInfoVolunteerRequest
(
  Guid VolunteerId,
  UpdateMainInfoDto Dto
)
{
  public UpdateMainInfoVolunteerCommand ToCommand() => 
  new( 
    VolunteerId, 
    //FullNameDto, 
    //Email, 
    Dto.Description, 
    //Experience, 
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
    RuleFor(r => r.Dto.Description).NotEmpty().MustBeValueObject(Description.Create);//.When(r => !string.IsNullOrEmpty(r.Description));
    RuleFor(r => r.Dto.Phone).NotEmpty().MustBeValueObject(Phone.Create); 
    //RuleFor(c => c.FullNameDto).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
  }
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
  //FullNameDto FullNameDto,
  //string Email,
  string Description,
  //int Experience,
  string Phone
);