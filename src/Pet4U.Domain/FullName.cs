namespace Pet4U.Domain
{
  public record FullName
  {
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }  = null!;
    public string MiddleName { get; set; } = null!;

  }
}