using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules;

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