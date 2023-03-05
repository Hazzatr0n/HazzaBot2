namespace Bot.Discord;

public class Embed
{
    public string Title;
    public string Description;
    public string Url;
    public EmbedAuthor Author;

    public Embed(string title, string description, string url, EmbedAuthor auth)
    {
        Title = title;
        Description = description;
        Url = url;
        Author = auth;
    }
}