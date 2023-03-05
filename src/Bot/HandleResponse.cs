using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Bot.Discord;

namespace Bot;

public class HandleResponse
{
    private HttpResponseType Type { get; set; }
    private string ResponseBody { get; set; } = "";

    public HandleResponse(HttpResponseType type)
    {
        Type = type;
    }

    public HandleResponse(HttpResponseType type, IResponse body)
    {
        Type = type;
        ResponseBody = body.GetJsonResponse();
    }

    public HandleResponse()
    {
        // If nothing is passed, assume that an error occured
        Type = HttpResponseType.Malformed;
    }

    public APIGatewayProxyResponse GetResponseObj()
    {
        return new APIGatewayProxyResponse()
        {
            StatusCode = (int)Type,
            Body = ResponseBody != string.Empty ? ResponseBody : null,
            Headers = new Dictionary<string, string>()
            {
                { "content-type", "application/json" }
            }
        };
    }
}