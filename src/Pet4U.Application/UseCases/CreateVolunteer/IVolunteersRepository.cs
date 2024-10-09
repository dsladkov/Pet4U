using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface IVolunteersRepository
{
  Task<Result<Guid, Error>> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
  Task<Result<Volunteer?>>GetByIdAsync(VolunteerId volunteerId, CancellationToken cancellationToken = default);
}