using Pet4U.Domain.Species;
using Pet4U.Domain.ValueObjects;

namespace Pet4U.Domain.Species;

public class Breed : Entity<Guid>
{
  public Breed(Guid id) : base(id){}
  public Breed(Guid id, string title, string description) : base(id)
  {
    Title = title;
    Description = description;
  }

  public string Title { get; private set; } = null!;
  public string Description { get; private set; } = null!;
  

}