using Pet4U.Domain;

namespace Pet4U.Domain.Volunteers
{
  //Part of the JSON column
  public record PaymentInfo
  {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public PaymentInfo(){}
    private PaymentInfo(string title, string description)
    {
      Title = title;
      Description = description;
    }

    public static PaymentInfo Create(string title, string description) => new(title, description);
  }
}