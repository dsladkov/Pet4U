namespace Pet4U.Domain;

public record PaymentInfoList
{
  public List<PaymentInfo> Data {get; set;} = [];
}