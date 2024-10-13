
using Pet4U.Domain.ValueObjects;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface ICreateVolunteerHandler
{
  Task<Result<Guid>> HandleAsync(CreateVolunteerCommand command, CancellationToken cancellationToken);
}
