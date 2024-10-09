using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface ICreateVolunteerHandler
{
  Task<Result<Guid, Error>> HandleAsync(CreateVolunteerCommand command, CancellationToken cancellationToken);
}
