namespace Pet4U.Domain.Modules;

public record VolunteerId
{
  private VolunteerId(Guid id)
  {
    Value = id;
  }

  public Guid Value { get; }

  public VolunteerId GetNew(Guid id) => new (id);
  public VolunteerId GetEmpty() => new (Guid.Empty);

  // ef core
  public static VolunteerId Create(Guid id) => new (id);
}