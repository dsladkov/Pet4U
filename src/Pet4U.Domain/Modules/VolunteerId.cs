namespace Pet4U.Domain.Modules;

public record VolunteerId
{
  private VolunteerId(Guid id)
  {
    Value = id;
  }

  public Guid Value { get; }

  public static VolunteerId New() => new (Guid.NewGuid());
  public static VolunteerId Empty() => new (Guid.Empty);
  public static VolunteerId Create(Guid id) => new(id);
}