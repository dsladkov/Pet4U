using Pet4U.Domain.Ids;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface IVolunteersRepository
{
  Task<Result<Guid>> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
  Task<Result<Volunteer?>>GetByIdAsync(VolunteerId volunteerId, CancellationToken cancellationToken = default);
}