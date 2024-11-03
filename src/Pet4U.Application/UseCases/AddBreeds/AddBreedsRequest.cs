namespace Pet4U.Application.UseCases.AddBreeds;

public record AddBreedsRequest(IReadOnlyCollection<BreedDto> breeds)
{
  public static AddBreedCommand ToCommand(Guid id, IReadOnlyCollection<BreedDto> breedDtos) => new (id, breedDtos);
}

public record AddBreedCommand(Guid id, IReadOnlyCollection<BreedDto> breedDtos);

public record BreedDto(string Title, string Description);