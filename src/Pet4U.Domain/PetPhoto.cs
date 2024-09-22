namespace Pet4U.Domain
{
  public class PetPhoto
  {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Path { get; private set; } = null!;
    public bool IsMain { get; private set; }
  }
}