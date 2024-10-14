
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public record CreateVolunteerRequest
(
  string FirstName,
  string LastName,
  string MiddleName,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfo>? PaymentInfos,
  IReadOnlyCollection<SocialNetwork>? SocialNetworks
)
{
  public CreateVolunteerCommand ToCommand() => new(FirstName, LastName, MiddleName, Email, Description, Experience, Phone, PaymentInfos, SocialNetworks);
}

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
  public CreateVolunteerRequestValidator()
  {
    //RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100);
    //RuleFor(c => c.LastName).NotEmpty().MaximumLength(100);
    RuleFor(c => c.Email).EmailAddress();
    RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);
    RuleFor(c => new {c.FirstName, c.LastName, c.MiddleName }).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
  }
}


public record CreateVolunteerCommand
(
  string FirstName,
  string LastName,
  string MiddleName,
  string Email,
  string Description,
  int Experience,
  string Phone,
  IReadOnlyCollection<PaymentInfo>? PaymentInfos,
  IReadOnlyCollection<SocialNetwork>? SocialNetworks
);