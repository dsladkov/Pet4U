using Microsoft.EntityFrameworkCore;
using Pet4U.Application.UseCases.CreateSpecies;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
  private readonly ApplicationDbContext _dbContext;

  public SpeciesRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }


  public async Task<Result<Guid>> AddAsync(
    Species species, 
    CancellationToken cancellationToken = default)
  {
    await _dbContext.Species.AddAsync(species, cancellationToken);

    await _dbContext.SaveChangesAsync(cancellationToken);

    return species;
  }

  public async Task<Result<Species?>>GetByIdAsync(
    SpeciesId speciesId, 
    CancellationToken cancellationToken = default)
  {
    var species = await _dbContext.Species
              .Include(s => s.Breeds)
              .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

    if(speciesId is null)
      return Errors.General.NotFound(speciesId!);

    return species;
  }

  public async Task<Result<Guid>> Save(
    Species species, 
    CancellationToken cancellationToken = default)
  {

    _dbContext.Species.Attach(species); //This line of code for track entity. Ef core should do it by yourself. 

    await _dbContext.SaveChangesAsync(cancellationToken);

    return species;
  }

  public async Task<Result<Species?>>Delete(Species species, CancellationToken cancellationToken = default)
  {
    _dbContext.Species.Remove(species);

    await _dbContext.SaveChangesAsync(cancellationToken);

    return species;
  }
}