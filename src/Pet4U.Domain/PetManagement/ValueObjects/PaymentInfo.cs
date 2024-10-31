using Pet4U.Domain;
using Pet4U.Domain.Shared;

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

    public static Result<PaymentInfo> Create(string title, string description)
    {

      if(string.IsNullOrEmpty(title))
        return Errors.General.ValueIsInvalid("Title");

      if(string.IsNullOrEmpty(description))
        return Errors.General.ValueIsInvalid("Description");
      return new PaymentInfo(title,description);
    } //=> new(title, description);
  }
}