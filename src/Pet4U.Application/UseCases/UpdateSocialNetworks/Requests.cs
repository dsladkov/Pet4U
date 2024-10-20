
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdateSocialNetworks;

public record SocialNetworkDto
{
  public string Title { get; set;}
  public string Link { get; set; }
}

public record UpdateSocialNetworkListCommand
( Guid VolunteerId,
  IReadOnlyCollection<SocialNetworkDto> SocialNetworkDtos
);

public record UpdateSocialNetworkListDto(IReadOnlyCollection<SocialNetworkDto> SocialNetworkDtos);
public record UpdateSocialNetworkListRequest(Guid Id,UpdateSocialNetworkListDto Dto)
{
  public UpdateSocialNetworkListCommand ToCommand() => new(Id, Dto.SocialNetworkDtos );
}

public class UpdateSocialNetworkListVolunteerRequestValidator : AbstractValidator<UpdateSocialNetworkListRequest>
{
  public UpdateSocialNetworkListVolunteerRequestValidator()
  {
    RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleForEach(r => r.Dto.SocialNetworkDtos).NotNull();
  }
}
