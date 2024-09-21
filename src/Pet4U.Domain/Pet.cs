using System;
using System.Collections.Generic;

namespace Pet4U.Domain
{
  public class Pet
  {
    private List<PaymentInfo> _paymentInfo = [];

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nickname { get; private set; } = null!;
    public string Species { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Breed {get; private set;} = null!;
    public string Color { get; private set; } = null!;
    public string Health { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string Phone { get; private set; } = null!;
    public bool IsNeutered { get; private set; }
    public DateOnly Birthday { get; private set; }
    public bool IsVaccinated { get; private set; }
    public Status Status { get; private set; }
    public DateOnly CreateDate { get; private set; }

    public IReadOnlyCollection<PaymentInfo> PaymentInfos => _paymentInfo;
    public void AddPayment(PaymentInfo paymentInfo) => _paymentInfo.Add(paymentInfo);
  }
}