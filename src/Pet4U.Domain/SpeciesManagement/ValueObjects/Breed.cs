using Pet4U.Domain.Shared;

namespace Pet4U.Domain.SpeciesManagement.ValueObject;

public class Breed : Entity<Guid>, ISoftDeletable
{
  private bool _isDeleted = false;
  public Breed(Guid id) : base(id){}
  public Breed(Guid id, string title, string description) : base(id)
  {
    Title = title;
    Description = description;
  }

  public string Title { get; private set; } = null!;
  public string Description { get; private set; } = null!;

public void Delete()
    {
      if(!_isDeleted)
      {
        _isDeleted = true;
      }
    }

    public void Restore()
    {
      if(_isDeleted)
      {
        _isDeleted = false;
      }
    }
}