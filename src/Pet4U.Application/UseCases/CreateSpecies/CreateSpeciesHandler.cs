using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Pet4U.Domain.Shared;

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

  }
}