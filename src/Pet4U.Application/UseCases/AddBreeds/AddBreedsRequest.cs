namespace Pet4U.Application.UseCases.AddBreeds;

public record AddBreedsRequest
{
  // public string Title { get; } = string.Empty;
  // public string Description { get; } = string.Empty;

  public IReadOnlyCollection<BreedDto> breeds;

  private AddBreedsRequest(IReadOnlyCollection<BreedDto> breedDtos)
  {
    // Title = title;
    // Description = description;
    breeds = breedDtos;
  }

  public static AddBreedCommand ToCommand(Guid id, IReadOnlyCollection<BreedDto> breedDtos) => new (id, breedDtos);
}

public record AddBreedCommand(Guid id, IReadOnlyCollection<BreedDto> breedDtos);

public record BreedDto(string Title, string Description);