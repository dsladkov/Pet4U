namespace Pet4U.Domain.Volunteers
{
  public class PetPhoto
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Path { get; set; } = null!;
    public bool IsMain { get; set; }
  }
}