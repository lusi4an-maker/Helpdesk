using System.Text.Json;
using System.Text.Json.Serialization;

namespace Helpdesk.Web.Json;

public static class JsonConfig
{
    public static readonly JsonSerializerOptions Options = new()
    {
        Converters = {new JsonStringEnumConverter()},
        PropertyNameCaseInsensitive = true
    };

}
