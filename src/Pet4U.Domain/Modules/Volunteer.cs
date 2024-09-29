using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules
{
  public class Volunteer : Entity<VolunteerId>
  {
    private List<Pet> _pets = [];
    private List<SocialNetwork> _socialNetworks = [];
    private List<PaymentInfo> _payments = [];

    private Volunteer(VolunteerId id) : base(id){}

    public Volunteer
      (
        VolunteerId id, 
        FullName fullName, 
        string email, 
        string description, 
        int experience, 
        string phone
      ) : base(id)
    {
      FullName = fullName;
      Email = email;
      Description = description;
      Experience = experience;
      Phone = phone;
    }
    public FullName FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int? Experience { get; private set; }
    public string Phone { get; set; } = null!;
    public IReadOnlyCollection<Pet> Pets => _pets;
    public void AddPet(Pet pet) => _pets.Add(pet);

    public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks;

    public IReadOnlyCollection<PaymentInfo> PaymentInfos => _payments;

    private int Counter(Status status) => _pets.Where(p => p.Status == status).Count();

    public int HomeFoundedPetsCounter() =>  Counter(Status.FoundHome);
    public int LookingForHomePetsCounter() => Counter(Status.LookingForHome);
    public int NeedHelpPetsCounter() => Counter(Status.NeedHelp);
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);
    public void AddPaymentInfo(PaymentInfo paymentInfo) => _payments.Add(paymentInfo);
   
  }
}