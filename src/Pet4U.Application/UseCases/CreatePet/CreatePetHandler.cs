using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
using Pet4U.Application.UseCases.CreateSpecies;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.CreatePet;

public class CreatePetHandler : ICreatePetHandler
{

    private readonly IVolunteersRepository _volunteerRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreatePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePetHandler
    (IVolunteersRepository volunteerRepository, 
    ISpeciesRepository speciesRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreatePetHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _speciesRepository = speciesRepository;
    _logger = logger;
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    CreatePetCommand command,
    CancellationToken cancellationToken
  )
  {
    var voluteerId = VolunteerId.Create(command.id);

    var voluteerResult = await _volunteerRepository.GetByIdAsync(voluteerId, cancellationToken);

    //var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

    if (voluteerResult.IsFailure)
      return voluteerResult.Error!;

    var speciesResult = await _speciesRepository.GetByNameAsync(command.Species, cancellationToken);
    
    if(speciesResult.IsFailure)
      return speciesResult.Error!;

    var breedId = speciesResult?.Value?.Breeds?.Where(b => b.Title == command.Breed)?.Select(r => r.Id)?.FirstOrDefault();

    if(breedId == null && breedId.Value != Guid.Empty)
      return Errors.General.ValueIsInvalid(command.Breed);
    
    var speciesBreed = new PetData(speciesResult.Value.Id,breedId.Value);


    var pet = new Pet(
      PetId.Create(Guid.NewGuid()),
      speciesBreed,
      command.Nickname, 
      command.Species, 
      command.Description,
      command.Breed,
      command.Color,
      command.Health,
      command.Address,
      command.Weight,
      command.Height,
      command.Phone,
      command.IsNeutered,
      command.Birthday,
      command.IsVaccinated,
      command.Status,
      command.CreateDate
    );

    voluteerResult.Value.AddPet(pet);

    var result = _volunteerRepository.Add(voluteerResult.Value, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Volunteer Id: {Id} has added new pet {id}", voluteerResult.Value.Id, pet.Id.Value);
    return result;
  }
}