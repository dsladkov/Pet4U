using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.UpdateMainInfo;

public interface IUpdateMainInfoHandler
{
  Task<Result<Guid>> HandleAsync
  (
    UpdateMainInfoVolunteerCommand command,
    CancellationToken cancellationToken
  );
}