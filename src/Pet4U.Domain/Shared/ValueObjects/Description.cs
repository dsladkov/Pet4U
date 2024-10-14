using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Shared.ValueObjects;

public record Description
{
  public string Value { get; }

  private Description(string value) => Value = value;

  public static Result<Description> Create(string value)
  {
    if(string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
      return Errors.General.ValueIsInvalid(nameof(Description));
    return new Description(value);
  }
}