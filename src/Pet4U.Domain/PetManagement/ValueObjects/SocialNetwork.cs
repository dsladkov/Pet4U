namespace Pet4U.Domain.Volunteers
{
  public record SocialNetwork
  {
    public string Title { get; set; } = null!;
    public string Link { get; set; } = null!;

    public static SocialNetwork Create(string title, string link) => new SocialNetwork{Title = title, Link = link};

  }
}