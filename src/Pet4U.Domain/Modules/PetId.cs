namespace Pet4U.Domain.Modules;

public record PetId
{
  private PetId(Guid id)
  {
    Value = id;
  }

  public Guid Value { get; }

  public static PetId GetNew(Guid id) => new (id);
  public static PetId GetEmpty() => new (Guid.Empty);

  // ef core
  //public static PetId Create(Guid id) => new (id);
}