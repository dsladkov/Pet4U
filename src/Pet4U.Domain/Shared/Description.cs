namespace Pet4U.Domain.Shared;

public record Description
{
  public string Value { get; }

  public Description(string value) => Value = value;

  public static Result<Description, Error> Create(string value)
  {
    if(string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
      return Errors.General.ValueIsInvalid(nameof(Description));
    return new Description(value);
  }
}