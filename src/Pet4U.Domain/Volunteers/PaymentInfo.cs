using Pet4U.Domain;

namespace Pet4U.Domain.Volunteers
{
  //Part of the JSON column
  public record PaymentInfo
  {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
  }
}