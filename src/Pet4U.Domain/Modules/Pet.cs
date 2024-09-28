using System;
using System.Collections.Generic;
using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules
{
  public class Pet : Entity<PetId>
  {
    private List<PetPhoto> _petPhoto = [];

    public Pet(PetId id) : base(id){}

    public Pet
    (
      PetId id,
      string nickname, 
      string Species, 
      string description,
      string breed,
      string color,
      string health,
      string address,
      double weight,
      double height,
      string phone,
      bool isNeutered,
      DateOnly? birthDay,
      bool isVaccinated,
      Status status,
      DateOnly createDate
    ) : base(id)
    {
      
    }
    
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
    public bool? IsNeutered { get; private set; }
    public DateOnly? Birthday { get; private set; }
    public bool IsVaccinated { get; private set; }
    public Status Status { get; private set; }
    public DateOnly CreateDate { get; private set; }
    public Volunteer? Volunteer { get; set; }

    // public IReadOnlyCollection<PaymentInfo> PaymentInfos => _paymentInfo;
    public IReadOnlyCollection<PetPhoto> PetPhotos => _petPhoto;

    public void AddPetPhoto(PetPhoto petPhoto) => _petPhoto.Add(petPhoto);
    // public void AddPayment(PaymentInfo paymentInfo) => _paymentInfo.Add(paymentInfo);
  }
}