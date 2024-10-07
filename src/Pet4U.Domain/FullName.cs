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

    public static FullName Create(string firstName, string lastName, string middleName)
    {
      return new FullName(firstName, lastName, middleName);
    }

  }
}