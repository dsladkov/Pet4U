namespace Pet4U.Domain.Shared;

public record Phone
{
  public string Value { get; }

  private Phone(string value)
  {
    Value = value;
  }
  public static Phone Create(string value)
        => new Phone(value);
  
}