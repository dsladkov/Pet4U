namespace Pet4U.Domain.Shared;

public record Description
{
  public string Value { get; }

  public Description(string value) => Value = value;

  public static Description Create(string value)
  {
    return new Description(value);
  }
}