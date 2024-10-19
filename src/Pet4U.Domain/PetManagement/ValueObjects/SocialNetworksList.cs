using Pet4U.Domain.Volunteers;

namespace Pet4U.Domain.ValueObjects;

public record SocialNetworksList 
{
  public IReadOnlyCollection<SocialNetwork> Data {get;}

  public SocialNetworksList(IReadOnlyCollection<SocialNetwork> socialNetworks) => Data = socialNetworks;


  // ef core
  public SocialNetworksList(){}
}