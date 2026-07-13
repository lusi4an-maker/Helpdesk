using System.Text.Json;

using Microsoft.Extensions.Configuration;

using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;
namespace Helpdesk.Web.Extensions;

public static class HttpResponseExtensions
{
    public static async Task<string?> LeerErrorAsync(this HttpResponseMessage response)
    {
        var responseBody = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(responseBody)) return null;
        try
        {
            var problemaBody = JsonSerializer.Deserialize<ValidationProblem>(responseBody, JsonConfig.Options);
            if (problemaBody?.Errors is { Count: > 0 })
            {
                var mensajes = problemaBody.Errors.Values.SelectMany(v => v);
                return string.Join(" ", mensajes);
            }
        }
        catch
        { }
        try
        {
            return JsonSerializer.Deserialize<string>(responseBody, JsonConfig.Options);
        }
        catch { }
        return null;
    }
}       
