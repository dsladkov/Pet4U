namespace Pet4U.Domain.Volunteers;

public record PaymentInfos
{
  public IReadOnlyCollection<PaymentInfo> Data {get;}

  public PaymentInfos(IEnumerable<PaymentInfo> paymentInfos) : base() => Data = paymentInfos.ToList();

  // ef core
  public PaymentInfos() {}
}