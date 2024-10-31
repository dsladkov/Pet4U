using Pet4U.Domain.Volunteers;

namespace Pet4U.Domain.ValueObjects;

public record SocialNetworks 
{
  public IReadOnlyCollection<SocialNetwork>? Data {get;}

  public SocialNetworks(IEnumerable<SocialNetwork>? socialNetworks) => Data = socialNetworks?.ToList();


  // ef core
  public SocialNetworks(){}
}