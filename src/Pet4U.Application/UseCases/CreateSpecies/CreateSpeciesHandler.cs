using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Application.UseCases.CreateSpecies;

public class CreateSpeciesHandler
{
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public CreateSpeciesHandler(ILogger<CreateSpeciesHandler> logger)
  {
    _logger = logger;
  }

  public async Task<Result<Guid>> HandleAsync(CreateSpeciesCommand command ,CancellationToken cancellationToken = default)
  {
    var species = Species.Create
    (
      id: SpeciesId.New(),
      title: command.Title,
      description: command.Description
    );

    if(species.IsFailure)
      return species.Error;
    
    return species.Value.Id.Value;
  }
}