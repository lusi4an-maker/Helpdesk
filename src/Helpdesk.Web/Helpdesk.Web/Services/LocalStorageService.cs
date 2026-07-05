using System.Text.Json;

using Microsoft.JSInterop;

namespace Helpdesk.Web.Services;

// Envuelve IJSRuntime para no repetir las llamadas de JS interop en cada componente/servicio que necesite persistir datos en el navegador.
public interface ILocalStorageService
{
    Task SetItemAsync(string key, string value);
    Task<string?> GetItemAsync(string key);
    Task RemoveItemAsync(string key);
}
public class LocalStorageService (IJSRuntime js) : ILocalStorageService
{
    //Seteo el item pasando la clave y el valor
    public async Task SetItemAsync(string key, string value)
    {
        await js.InvokeVoidAsync("localStorage.setItem", key, value);
    }
    public async Task<string?> GetItemAsync(string key)
    {
        //guardo el json en un nullable para manejar errores luego
        string? json = await js.InvokeAsync<string?>("localStorage.getItem", key);
        
        //si el json viene null, devolvemos
        if (string.IsNullOrEmpty(json))
            return default;

        return JsonSerializer.Deserialize<string>(json);
    }

    public async Task RemoveItemAsync(string key) 
    {
        await js.InvokeVoidAsync("localStorage.removeItem", key);
    }
    
}
