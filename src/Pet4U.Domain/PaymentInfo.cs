using Pet4U.Domain.Modules;

namespace Pet4U.Domain
{
  public class PaymentInfo
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
  }
}