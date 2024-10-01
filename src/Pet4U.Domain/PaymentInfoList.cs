namespace Pet4U.Domain;

public record PaymentInfoList
{
  private List<PaymentInfo> _paymentInfos = [];
  public List<PaymentInfo> Data => _paymentInfos;

  private PaymentInfoList(IReadOnlyCollection<PaymentInfo> paymentInfos) : base() => _paymentInfos = paymentInfos.ToList();

  public static PaymentInfoList Create(IReadOnlyCollection<PaymentInfo> paymentInfos) => new(paymentInfos);

  // ef core
  public PaymentInfoList() {}
}