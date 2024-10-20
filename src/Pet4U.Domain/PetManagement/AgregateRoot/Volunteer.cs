
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Domain.PetManagement.AgregateRoot
{
    public sealed class Volunteer : Entity<VolunteerId>, ISoftDeletable
  {
    private bool _isDeleted = false;
    private List<Pet> _pets = [];
    private Volunteer(VolunteerId id) : base(id){}

    private Volunteer
      (
        VolunteerId id, 
        FullName fullName, 
        string email, 
        Description description, 
        int experience, 
        Phone phone,
        IReadOnlyCollection<SocialNetwork>? socialNetworks,
        IReadOnlyCollection<PaymentInfo>? paymentInfos
      ) : base(id)
    {
      FullName = fullName;
      Email = email;
      Description = description;
      Experience = experience;
      Phone = phone;
      SocialNetworks =  new(socialNetworks);
      PaymentInfos =   new(paymentInfos);
    }
    public FullName FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public int? Experience { get; private set; }
    public Phone Phone { get; private set; } = null!;

    public SocialNetworks SocialNetworks { get; private set; }
    public PaymentInfos PaymentInfos { get; private set; }
  
    public IReadOnlyCollection<Pet> Pets => _pets;
    public void AddPet(Pet pet) => _pets.Add(pet);

    // public void UpdateSocialNetworks(IReadOnlyCollection<SocialNetwork> socialNetworks) => SocialNetworks = new(socialNetworks);
    public void UpdateSocialNetworks(SocialNetworks socialNetworks)
    {
      SocialNetworks = socialNetworks;
    } //=> SocialNetworks = socialNetworks;

    public void UpdatePaymentInfos(PaymentInfos paymentInfos)=> PaymentInfos = paymentInfos;
    
    private int Counter(Status status) => _pets.Where(p => p.Status == status).Count();

    public int HomeFoundedPetsCounter() =>  Counter(Status.FoundHome);
    public int LookingForHomePetsCounter() => Counter(Status.LookingForHome);
    public int NeedHelpPetsCounter() => Counter(Status.NeedHelp);

    public void UpdateMainInfo(FullName fullname, string email, int experience, Description description, Phone phone)
    {
      FullName = fullname;
      Email = email;
      Experience = experience;
      Description = description;
      Phone = phone;
    }

    public static Result<Volunteer> Create
    (
      VolunteerId id,
      FullName fullName,
      string email,
      Description description,
      int experience,
      Phone phone,
      IReadOnlyCollection<SocialNetwork>? socialNetworks,
      IReadOnlyCollection<PaymentInfo>? paymentInfos
      )
    {
      // if(id is null)
      //   return "Invalid VolunterId";
      
      // if(fullName is null)
      //   return "Invalid FullName";

      // if(description is null)
      //   return "Invalid description";
      // if(phone is null)
      //   return "Invalid phone number";

      return new Volunteer
      (
        id: id, 
        fullName: fullName, 
        email: email, 
        description: description, 
        experience: experience,
        phone: phone,
        socialNetworks: socialNetworks, 
        paymentInfos: paymentInfos
      );
    }

    public static Result<Volunteer> Update
    (
      VolunteerId id,
      FullName fullName,
      string email,
      Description description,
      int experience,
      Phone phone,
      IReadOnlyCollection<SocialNetwork>? socialNetworks,
      IReadOnlyCollection<PaymentInfo>? paymentInfos
      )
    {
      return new Volunteer
      (
        id: id, 
        fullName: fullName, 
        email: email, 
        description: description, 
        experience: experience,
        phone: phone,
        socialNetworks: socialNetworks, 
        paymentInfos: paymentInfos
      );
    }

    public void Delete()
    {
      if(!_isDeleted)
        _isDeleted = true;
    }

    public void Restore()
    {
      if(_isDeleted)
        _isDeleted = false;
    }

    public static implicit operator Result<Guid>(Volunteer volunteer) => volunteer.Id.Value;
  }
}