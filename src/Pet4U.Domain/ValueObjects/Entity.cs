namespace Pet4U.Domain.ValueObjects;

public abstract class Entity<T> where T : notnull
{
  public T Id { get; set; }

  protected Entity(T id) => Id = id;
}