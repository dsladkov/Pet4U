namespace Pet4U.Domain.Shared.Ids;

public record PetId
{
  private PetId(Guid id)
  {
    Value = id;
  }

  public Guid Value { get; }

  public static PetId GetNew() => new (Guid.NewGuid());
  public static PetId GetEmpty() => new (Guid.Empty);

  // ef core
  public static PetId Create(Guid id) => new (id);

  public static implicit operator Guid (PetId petId)
  {
    ArgumentNullException.ThrowIfNull(petId);
    return petId.Value;
  }
}