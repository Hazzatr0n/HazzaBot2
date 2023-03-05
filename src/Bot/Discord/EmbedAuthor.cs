using System.Text.Json.Serialization;

namespace Bot.Discord;

public struct EmbedAuthor
{
    public string Name;
    [JsonPropertyName("icon_url")]
    public string IconUrl;
}