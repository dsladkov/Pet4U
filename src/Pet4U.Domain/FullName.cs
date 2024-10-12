using Pet4U.Domain.Shared;
using static Pet4U.Domain.Shared.Errors;

namespace Pet4U.Domain
{
  public record FullName
  {
    public string FirstName { get; } = null!;
    public string LastName { get; }  = null!;
    public string MiddleName { get;} = null!;

    private FullName(string firstName, string lastName, string middleName)
    {
      FirstName = firstName;
      LastName = lastName;
      MiddleName = middleName;

    }

    public static Result<FullName> Create(string firstName, string lastName, string middleName)
    {
      if(string.IsNullOrWhiteSpace(firstName))
        return Errors.General.ValueIsInvalid("first name");//Error.Validation("value.is.invalid", "Invalid first name wasn't accepted");

      if(firstName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        return General.LengthIsInvalid(firstName);

      if(string.IsNullOrWhiteSpace(lastName))
        return General.ValueIsInvalid("last name"); //Error.Validation("value.is.invalid", "Invalid lst name wasn't accepted");

      if(lastName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        return General.LengthIsInvalid(lastName);
      
      return new FullName(firstName, lastName, middleName);
    }

  }
}