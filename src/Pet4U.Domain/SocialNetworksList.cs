namespace Pet4U.Domain;

public record SocialNetworksList 
{
  private List<SocialNetwork> _socialNetworks = [];
  public IReadOnlyCollection<SocialNetwork> Data => _socialNetworks;

  private SocialNetworksList(IReadOnlyCollection<SocialNetwork> socialNetworks) : base() => _socialNetworks = socialNetworks.ToList();

  public static SocialNetworksList Create(IReadOnlyCollection<SocialNetwork> socialNetworks) => new(socialNetworks);

  // ef core
  public SocialNetworksList(){}
}