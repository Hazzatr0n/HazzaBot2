using System.Text.Json;

namespace Bot.Discord;

public class ResponseMessage : IResponse
{
    public int Type { get; set; } = 4;
    public Message Data { get; set; }
    public string GetJsonResponse() => JsonSerializer.Serialize(this);

    public ResponseMessage(string content, bool isPrivate)
    {
        // Just make a message with content and nothing else
        Data = new Message()
        {
            Content = content,
            Flags = isPrivate ? 1 << 6 : 0,
            Embeds = null
        };
    }

    public ResponseMessage(string content, bool isPrivate, Embed[] embeds)
    {
        // Construct message with an embed
        Data = new Message()
        {
            Content = content,
            Flags = isPrivate ? 1 << 6 : 0,
            Embeds = embeds
        };
    }
}