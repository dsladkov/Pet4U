using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.ValueObject;

namespace Pet4U.Domain.SpeciesManagement.AgregateRoot;

public class Species : Entity<SpeciesId>, ISoftDeletable
{
  private bool _isDeleted = false;
  public Species(SpeciesId id) : base(id){}

  private Species
  (
    SpeciesId id, 
    string title,
    string description//, 
    //List<Breed> breeds
  ) : base(id)

  {
    Title = title;
    Description = description;
    //_breeds = breeds;
  }

  private List<Breed> _breeds = [];
  public IReadOnlyCollection<Breed> Breeds => _breeds;

  public string Title { get; private set; } = null!;
  public string Description { get; private set; } = null!;

  public void AddBreeds(IReadOnlyCollection<Breed> breeds) => _breeds = breeds.ToList();

  public static Result<Species> Create
  (
    SpeciesId id,
    string title,
    string description//, 
   // List<Breed> breeds
  )
  {
    if(string.IsNullOrEmpty(title))
      return Errors.General.ValueIsInvalid("title");

    if(title.Length > Constants.MAX_LOW_TEXT_LENGTH)
      return Errors.General.LengthIsInvalid("title");

    if(string.IsNullOrWhiteSpace(description))
      return Errors.General.ValueIsInvalid("description");

    if(description.Length > Constants.MAX_HIGH_TEXT_LENGTH)
      return Errors.General.LengthIsInvalid("description");
    
    return new Species(id, title, description);
  }

public void Delete()
    {
      if(!_isDeleted)
      {
        _isDeleted = true;
        foreach(var breed in _breeds)
        {
          breed.Delete();
        }
      }
    }

    public void Restore()
    {
      if(_isDeleted)
      {
        _isDeleted = false;
        foreach(var breed in _breeds)
        {
          breed.Restore();
        }
      }
    }

    public static implicit operator Result<Guid>(Species species) => species.Id.Value;
}