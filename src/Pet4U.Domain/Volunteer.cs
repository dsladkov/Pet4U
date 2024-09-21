namespace Pet4U.Domain
{
  public class Volunteer
  {
    private List<Pet> _pets = [];
    private List<SocialNetwork> _socialNetworks = [];
    private List<PaymentInfo> _payments = [];

    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Experience { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public IReadOnlyCollection<Pet> Pets => _pets;
    public void AddPet(Pet pet) => _pets.Add(pet);

    public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks;

    private int Counter(Status status) => _pets.Where(p => p.Status == status).Count();

    public int HomeFoundedPetsCounter() =>  Counter(Status.FoundHome);
    public int LookingForHomePetsCounter() => Counter(Status.LookingForHome);
    public int NeedHelpPetsCounter() => Counter(Status.NeedHelp);
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);
    public void AddPaymentInfo(PaymentInfo paymentInfo) => _payments.Add(paymentInfo);
   
  }
}