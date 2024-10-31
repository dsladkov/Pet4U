using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Volunteers
{
  public record SocialNetwork
  {
    public string? Title { get;}
    public string? Link { get;}
    public SocialNetwork(){}

    private SocialNetwork(string title, string link)
    {
      Title = title;
      Link = link;
    }

    public static Result<SocialNetwork> Create(string title, string link)
    {
      if(string.IsNullOrEmpty(title))
        return Errors.General.ValueIsInvalid("Title");

      if(string.IsNullOrEmpty(link))
        return Errors.General.ValueIsInvalid("Link");
      return new SocialNetwork(title,link);
    }

  }
}