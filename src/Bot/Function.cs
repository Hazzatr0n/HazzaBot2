using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Bot.Discord;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Bot
{
    public class Function
    {
        private bool _isInitialised = false;
        private Security _botSecurity;

        private void InitialiseInstance()
        {
            _botSecurity = new Security(System.Environment.GetEnvironmentVariable("PUBLICKEY"));

            _isInitialised = true;
        }
        

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apiProxyEvent, ILambdaContext context)
        {
            // Check that the bot has been initialised before
            if (!_isInitialised) InitialiseInstance();

            // If the request has the right headers, then allow for security check
            string timestamp = "";
            string ed = "";
            bool hasHeaders = apiProxyEvent.Headers.TryGetValue("x-signature-timestamp", out timestamp) &&
                               apiProxyEvent.Headers.TryGetValue("x-signature-ed25519", out ed);
            
            
            // The default return is malformed
            if (!hasHeaders) return new HandleResponse().GetResponseObj();
            
            // Start the security check
            string rawBody = apiProxyEvent.Body;
            var securityAwaiter = Task.Run(() => _botSecurity.CheckValidity(rawBody,ed, timestamp));

            var jsonDoc = await JsonDocument.ParseAsync(new MemoryStream(Encoding.UTF8.GetBytes(rawBody)));
            var jsonRoot = jsonDoc.RootElement;
            
            // Check which interaction
            HandleResponse response;
            switch (jsonRoot.GetProperty("type").GetInt16())
            {
                case (int)InteractionType.Ping:
                    // PONG
                    response = new HandleResponse(
                        HttpResponseType.Ok,
                        new ResponsePong()
                    );
                
                    break;
                case (int)InteractionType.ApplicationCommand:
                    response = new HandleResponse();
                    break; 
                default:
                    // Unknown command
                    response = new HandleResponse();
                    break;
            }
            
            jsonDoc.Dispose();
            return await securityAwaiter
                ? response.GetResponseObj()
                : new HandleResponse(HttpResponseType.NoPermission).GetResponseObj();
        }
    }
}
