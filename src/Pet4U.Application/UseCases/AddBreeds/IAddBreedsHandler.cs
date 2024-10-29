using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.AddBreeds;

public interface IAddBreedsHandler
{
  Task<Result<Guid>> HandleAsync(AddBreedCommand command, CancellationToken cancellationToken = default);
}