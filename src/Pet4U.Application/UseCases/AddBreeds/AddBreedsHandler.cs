using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.AddBreeds;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.ValueObject;

namespace Pet4U.Application.UseCases.CreateSpecies;

public class AddBreedsHandler : IAddBreedsHandler
{
    private readonly ILogger<AddBreedsHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;

    public AddBreedsHandler(ILogger<AddBreedsHandler> logger, ISpeciesRepository speciesRepository)
  {
    _logger = logger;
    _speciesRepository = speciesRepository;
  }

  public async Task<Result<Guid>> HandleAsync(AddBreedCommand command, CancellationToken cancellationToken = default)
  {

    var species = await _speciesRepository.GetByIdAsync(SpeciesId.Create(command.id), cancellationToken);


    if(species.IsFailure)
      return species.Error;

    var breeds = command.breedDtos.Select(b => new Breed( Guid.Empty, b.Title, b.Description));

    var addedBreeds = species?.Value?.AddBreeds(breeds.ToList());
    if(addedBreeds.IsFailure)
      return addedBreeds.Error;

    var speciesId = await _speciesRepository.Save(species.Value, cancellationToken);
    _logger.LogInformation("Species : {Id} has been created", species.Value.Id);

    return species.Value.Id.Value;
  }
}