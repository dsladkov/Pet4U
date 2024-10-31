namespace Pet4U.Application.UseCases.CreateSpecies;

public record CreateSpeciesRequest(string title, string description)
{
  public static CreateSpeciesCommand ToCommand(string title, string description) => new (title, description);
}

public record CreateSpeciesCommand(string Title, string Description);