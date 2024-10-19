using Microsoft.EntityFrameworkCore;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;

namespace Pet4U.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
  private readonly ApplicationDbContext _dbContext;

  public VolunteersRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }


  public async Task<Result<Guid>> AddAsync(
    Volunteer volunteer, 
    CancellationToken cancellationToken = default)
  {
    await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

    await _dbContext.SaveChangesAsync(cancellationToken);

    return volunteer;
  }

  public async Task<Result<Volunteer?>>GetByIdAsync(
    VolunteerId volunteerId, 
    CancellationToken cancellationToken = default)
  {
    var volunteer = await _dbContext.Volunteers
              .Include(v => v.Pets)
              .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

    if(volunteerId is null)
      return Errors.General.NotFound(volunteerId!);

    return volunteer;
  }

  public async Task<Result<Guid>> Update(
    Volunteer volunteer, 
    CancellationToken cancellationToken = default)
  {
    var entries1 = _dbContext.ChangeTracker.Entries<Volunteer>();

    _dbContext.Update(volunteer);

    var entries2 = _dbContext.ChangeTracker.Entries<Volunteer>();

    await _dbContext.SaveChangesAsync(cancellationToken);

    var entries3 = _dbContext.ChangeTracker.Entries<Volunteer>();

    return volunteer;
  }
}