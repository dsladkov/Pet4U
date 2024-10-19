using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.DeleteVolunteer;

public interface IDeleteVolunteerHandler
{
  Task<Result<Guid>> HandleAsync
  (
    DeleteVolunteerCommand command,
    CancellationToken cancellationToken
  );
}