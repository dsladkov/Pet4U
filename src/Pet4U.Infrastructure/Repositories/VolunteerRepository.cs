using Microsoft.EntityFrameworkCore;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Modules;

namespace Pet4U.Infrastructure.VolunteerRepository;

public class VolunteerRepository : IVolunteerRepository
{
  private readonly ApplicationDbContext _dbContext;

  public VolunteerRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }


  public async Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
  {
    await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

    await _dbContext.SaveChangesAsync();

    return volunteer.Id.Value;
  }

  public async Task<Volunteer?>GetByIdAsync(VolunteerId volunteerId, CancellationToken cancellationToken = default)
  {
    var volunteer = await _dbContext.Volunteers
              .Include(v => v.Pets)
              .FirstOrDefaultAsync(v => v.Id == volunteerId,cancellationToken);
    return volunteer;
  }
}