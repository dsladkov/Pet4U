using Pet4U.Domain.Modules;

namespace Pet4U.Domain
{
  //Part of the JSON column
  public record PaymentInfo
  {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
  }
}