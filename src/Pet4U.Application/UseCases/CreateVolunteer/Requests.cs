
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;
using Pet4U.Application.UseCases.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;
public record SocialNetworkDto(string title, string link);
public record PaymentInfoDto(string title, string description);
public record CreateVolunteerCommand
(
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfoDto>? PaymentInfos,
  IReadOnlyCollection<SocialNetworkDto> SocialNetworks
);
public record CreateVolunteerRequest
(
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfoDto> PaymentInfos,
  IReadOnlyCollection<SocialNetworkDto> SocialNetworks
)
{
  public CreateVolunteerCommand ToCommand() => new(FullNameDto, Email, Description, Experience, Phone, PaymentInfos, SocialNetworks);
}

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
  public CreateVolunteerRequestValidator()
  {
    RuleFor(c => c.Email).EmailAddress().WithError(Errors.General.ValueIsInvalid("Email"));//.WithMessage(Errors.General.ValueIsInvalid("email").Serialize());
    RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);
    // RuleFor(c => new {c.FirstName, c.LastName, c.MiddleName }).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    RuleFor(c => c.FullNameDto).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    RuleFor(c => c.PaymentInfos).ForEach(x => x.MustBeValueObject(x => PaymentInfo.Create(x.title, x.description)));
    RuleFor(c => c.SocialNetworks).ForEach(x => x.MustBeValueObject(x => SocialNetwork.Create(x.title, x.link)));
  }
}


