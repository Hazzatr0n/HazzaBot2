using System.Text.Json;

namespace Bot.Discord;

public class ResponsePong : IResponse
{
    public int Type { get; set; } = 1;

    public string GetJsonResponse() => JsonSerializer.Serialize(this, IResponse.Options);
}