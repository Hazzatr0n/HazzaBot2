using System.Text.Json;

namespace Bot.Discord;

public interface IResponse
{
    // So we don't have to write it out each time
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };
    public int Type { get; set; }
    public string GetJsonResponse();
}