namespace Pet4U.Domain;

public record SocialNetworksList 
{
  public IReadOnlyCollection<SocialNetwork> Data;

  public SocialNetworksList(IReadOnlyCollection<SocialNetwork> socialNetworks) => Data = socialNetworks;//.ToList();


  // ef core
  public SocialNetworksList(){}
}