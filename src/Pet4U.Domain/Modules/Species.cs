using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules;

public class Species : Entity<SpeciesId>
{
  public Species(SpeciesId id) : base(id){}

  public Species
  (
    SpeciesId id, 
    string title,
    string description, 
    List<Breed> breeds
  ) : base(id)

  {
    Title = title;
    Description = description;
    _breeds = breeds;
  }

  private List<Breed> _breeds = [];
  public IReadOnlyCollection<Breed> Breeds => _breeds;

  public string Title { get; private set; } = null!;
  public string Description { get; private set; } = null!;

}