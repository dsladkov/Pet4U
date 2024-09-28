namespace Pet4U.Domain.Modules;

public record PetId
{
  private PetId(Guid id)
  {
    Value = id;
  }

  public Guid Value { get; }

  public PetId GetNew(Guid id) => new (id);
  public PetId GetEmpty() => new (Guid.Empty);

  // ef core
  public static PetId Create(Guid id) => new (id);
}