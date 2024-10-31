
using System.Data;
using FluentValidation;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdateSocialNetworks;

// public record SocialNetworkDto
// {
//   public string Title { get; set;} = null!;
//   public string Link { get; set; } = null!;
// }

public record UpdateSocialNetworkListCommand
( Guid VolunteerId,
  IReadOnlyCollection<SocialNetworkDto> SocialNetworkDtos
);

public record UpdateSocialNetworksDto(IReadOnlyCollection<SocialNetworkDto> socialNetworkDtos);
public record UpdateSocialNetworksRequest(Guid Id,UpdateSocialNetworksDto Dto)
{
  public UpdateSocialNetworkListCommand ToCommand() => new(Id, Dto.socialNetworkDtos );
}

public class UpdateSocialNetworkListVolunteerRequestValidator : AbstractValidator<UpdateSocialNetworksRequest>
{
  public UpdateSocialNetworkListVolunteerRequestValidator()
  {
    RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleForEach(r => r.Dto.socialNetworkDtos).NotNull();
  }
}
