namespace Pet4U.Domain.Shared;

public record Phone
{
  public string Value { get; }

  private Phone(string value)
  {
    Value = value;
  }
  public static Result<Phone, Error> Create(string value)
  {
    if(string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
      return Errors.General.ValueIsInvalid(nameof(Phone)); //Error.Validation("value.is.invalid", "Ivalid phone info");
    return new Phone(value);
  }
}