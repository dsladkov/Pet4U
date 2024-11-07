using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Application.UseCases.CreateSpecies;

public class CreateSpeciesHandler : ICreateSpeciesHandler
{
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeciesHandler(ILogger<CreateSpeciesHandler> logger, IUnitOfWork unitOfWork,ISpeciesRepository speciesRepository)
  {
    _logger = logger;
    _speciesRepository = speciesRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<Guid>> HandleAsync(CreateSpeciesCommand command, CancellationToken cancellationToken = default)
  {

    var speciesByName = await _speciesRepository.GetByNameAsync(command.Title, cancellationToken);
    if (!speciesByName.IsSuccess)
      return Errors.General.ValueIsInvalid(speciesByName?.Value?.Title);
      
    var species = Species.Create
    (
      id: SpeciesId.New(),
      title: command.Title,
      description: command.Description
    );

    if(species.IsFailure)
      return species.Error;

    var speciesId = _speciesRepository.Add(species.Value);
    await _unitOfWork.SaveChanges(cancellationToken);
    _logger.LogInformation("Species : {Id} has been created", species.Value.Id);

    return species.Value.Id.Value;
  }
}