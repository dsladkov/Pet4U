using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreateSpecies;

public interface ICreateSpeciesHandler
{
  Task<Result<Guid>> HandleAsync(CreateSpeciesCommand command, CancellationToken cancellationToken = default);
}