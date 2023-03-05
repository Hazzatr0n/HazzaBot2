#nullable enable
namespace Bot.Discord;

public class Message
{
    public bool Tts = false;
    public string Content = "";
    public int Flags = 0;
    public Embed[]? Embeds;
}