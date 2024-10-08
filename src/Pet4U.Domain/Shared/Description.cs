namespace Pet4U.Domain.Shared;

public record Description
{
  public string Value { get; }

  public Description(string value) => Value = value;

  public static Result<Description> Create(string value)
  {
    if(string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
      return "Invalid description";
    return new Description(value);
  }
}