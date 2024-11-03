using System.Runtime.Intrinsics.X86;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.ValueObject;

namespace Pet4U.Domain.SpeciesManagement.AgregateRoot;

public class Species : Entity<SpeciesId>
{
  public Species(SpeciesId id) : base(id){}

  private Species
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

  public Result<Guid> AddBreeds(IReadOnlyCollection<Breed> breeds)
  {
      var result =
            from inb  in _breeds
            join exb in breeds
              on inb.Title equals exb.Title
            select inb.Title;
      
      if(result != null && result.Any())
      {
        return Errors.General.ValueIsInvalid(string.Join(", ", result));
      }
    _breeds.AddRange(breeds);
    return this.Id.Value;
  }

  public static Result<Species> Create
  (
    SpeciesId id,
    string title,
    string description, 
    List<Breed> breeds
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
    
    return new Species(id, title, description, breeds);
  } 

}