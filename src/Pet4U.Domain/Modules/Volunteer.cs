using System.ComponentModel.DataAnnotations.Schema;
using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules
{
  public class Volunteer : Entity<VolunteerId>
  {
    private List<Pet> _pets = [];
    private Volunteer(VolunteerId id) : base(id){}

    public Volunteer
      (
        VolunteerId id, 
        FullName fullName, 
        string email, 
        string description, 
        int experience, 
        string phone,
        IEnumerable<SocialNetwork> socialNetworks,
        IEnumerable<PaymentInfo> paymentInfos
      ) : base(id)
    {
      FullName = fullName;
      Email = email;
      Description = description;
      Experience = experience;
      Phone = phone;
      SocialNetworks =  SocialNetworksList.Create(socialNetworks.ToList());
      PaymentInfos =   PaymentInfoList.Create(paymentInfos.ToList());
    }
    public FullName FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int? Experience { get; private set; }
    public string Phone { get; set; } = null!;

    public SocialNetworksList? SocialNetworks { get; private set; }
    public PaymentInfoList? PaymentInfos { get; private set; }
    public IReadOnlyCollection<Pet> Pets => _pets;
    public void AddPet(Pet pet) => _pets.Add(pet);
    private int Counter(Status status) => _pets.Where(p => p.Status == status).Count();

    public int HomeFoundedPetsCounter() =>  Counter(Status.FoundHome);
    public int LookingForHomePetsCounter() => Counter(Status.LookingForHome);
    public int NeedHelpPetsCounter() => Counter(Status.NeedHelp);
  }
}