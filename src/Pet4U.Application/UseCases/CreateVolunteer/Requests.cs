using Pet4U.Domain;
using Pet4U.Domain.Modules;

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