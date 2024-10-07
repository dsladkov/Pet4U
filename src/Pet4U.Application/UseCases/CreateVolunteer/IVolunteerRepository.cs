using Pet4U.Domain.Modules;

namespace Pet4U.Application.UseCases.CreateVolunteer;

public interface IVolunteerRepository
{
  Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
  Task<Volunteer?>GetByIdAsync(VolunteerId volunteerId, CancellationToken cancellationToken = default);
}