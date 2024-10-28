namespace Pet4U.Application.UseCases.CreateSpecies;

public record CreateSpeciesRequest
{
  public string Title { get; } = string.Empty;
  public string Description { get; } = string.Empty;

  private CreateSpeciesRequest(string title, string description)
  {
    Title = title;
    Description = description;
  }

  public static CreateSpeciesCommand ToCommand(string title, string description) => new (title, description);
}

public record CreateSpeciesCommand(string Title, string Description);