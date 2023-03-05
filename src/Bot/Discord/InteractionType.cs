namespace Bot.Discord;

public enum InteractionType
{
    // This enum is for incoming interactions from Discord
    Ping = 1,
    ApplicationCommand = 2,
    MessageComponent = 3,
    ApplicationCommandAutocomplete = 4,
    ModalSubmit = 5
}