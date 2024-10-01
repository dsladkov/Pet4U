namespace Pet4U.Domain;

public record PaymentInfoList
{
  public IReadOnlyCollection<PaymentInfo> Data;

  public PaymentInfoList(IReadOnlyCollection<PaymentInfo> paymentInfos) : base() => Data = paymentInfos;

  // ef core
  public PaymentInfoList() {}
}