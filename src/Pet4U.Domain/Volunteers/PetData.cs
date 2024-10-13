namespace Pet4U.Domain.Modules;

public record PetData
{
  public SpeciesId SpeciesId { get; } = null!;
  public Guid BreedId { get; }

  public PetData(SpeciesId speciesId, Guid breedId)
  {
    SpeciesId = speciesId;
    BreedId = breedId;
  }
}