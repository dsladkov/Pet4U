using Pet4U.Domain.Shared;

namespace Pet4U.Domain
{
  public record FullName
  {
    public string FirstName { get; } = null!;
    public string LastName { get; }  = null!;
    public string MiddleName { get;} = null!;

    public FullName(string firstName, string lastName, string middleName)
    {
      FirstName = firstName;
      LastName = lastName;
      MiddleName = middleName;

    }

    public static Result<FullName> Create(string firstName, string lastName, string middleName)
    {
      if(string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        return "Invalid first name wasn't accepted";

      if(string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        return "Invalid lst name wasn't accepted";

      return new FullName(firstName, lastName, middleName);
    }

  }
}