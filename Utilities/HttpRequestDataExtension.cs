using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace Spendr.Utilities;

public static class HttpRequestDataExtensions
{
    public static async Task<HttpResponseData> CreateStringResponseAsync(this HttpRequestData req, HttpStatusCode statusCode, string message)
    {
        var response = req.CreateResponse(statusCode);
        await response.WriteStringAsync(message);
        return response;
    }

    public static async Task<HttpResponseData> CreateJsonResponseAsync<T>(this HttpRequestData req, HttpStatusCode statusCode, T content)
    {
        var response = req.CreateResponse(statusCode);
        response.Headers.Add("Content-Type", "application/json");

        var jsonContent = JsonSerializer.Serialize(content);
        await response.WriteStringAsync(jsonContent);

        return response;
    }
}
