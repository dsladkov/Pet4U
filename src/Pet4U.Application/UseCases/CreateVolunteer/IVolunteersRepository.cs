

using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface IVolunteersRepository
{
  Task<Result<Guid>> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
  Task<Result<Volunteer?>>GetByIdAsync(VolunteerId volunteerId, CancellationToken cancellationToken = default);
  Task<Result<Guid>> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
}