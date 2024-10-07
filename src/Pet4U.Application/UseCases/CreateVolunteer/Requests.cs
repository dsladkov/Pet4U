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
);

public record CreateVolunteerCommand
(
  CreateVolunteerRequest CreateVolunteerRequest
);