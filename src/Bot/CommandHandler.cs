using System.Text.Json;
using Bot.Discord;

namespace Bot;

public class CommandHandler
{
    public string Operator;
    public JsonElement Input;
    public Database DatabaseSession;

    public CommandHandler(string op, JsonElement input, Database db)
    {
        Operator = op;
        Input = input;
        DatabaseSession = db;
    }

    private ResponseMessage _processGroupCreate()
    {
        
    }

    public IResponse ProcessCommand()
    {
        // Delegate each command to their respected function
        switch (Operator)
        {
            case "groupcreate":
                // do something
                return new ResponseMessage();
            default:
                break;
        }
    }
}