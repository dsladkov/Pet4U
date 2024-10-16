namespace Pet4U.Domain.Shared.Ids;

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

  public static implicit operator Guid (VolunteerId volunteerId)
  {
    ArgumentNullException.ThrowIfNull(volunteerId);
    return volunteerId.Value;
  }
}