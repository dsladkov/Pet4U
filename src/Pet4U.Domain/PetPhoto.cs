namespace Pet4U.Domain
{
  public class PetPhoto
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Path { get; set; } = null!;
    public bool IsMain { get; set; }
  }
}