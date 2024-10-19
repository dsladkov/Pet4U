using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.UpdateMainInfo;

public interface IUpdateSocialNetworks
{
  Task<Result<Guid>> HandleAsync
  (
    UpdateSocialNetworkListCommand command,
    CancellationToken cancellationToken
  );
}