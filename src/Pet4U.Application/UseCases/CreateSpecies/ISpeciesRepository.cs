using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Application.UseCases.CreateSpecies;

public interface ISpeciesRepository
{
  Task<Result<Guid>> AddAsync(
    Species species, 
    CancellationToken cancellationToken = default);

  Task<Result<Species?>>GetByIdAsync(
    SpeciesId speciesId, 
    CancellationToken cancellationToken = default);
  

  Task<Result<Guid>> Save(
    Species species, 
    CancellationToken cancellationToken = default);


  Task<Result<Species?>>GetByNameAsync(
    string title, 
    CancellationToken cancellationToken = default);

    Result<Guid> Add(Species species);
}