
using System.Net.Http.Json;
using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;

namespace Helpdesk.Web.Services;

public interface IUsuarioService
{
    Task<UsuarioDto[]> GetUsuariosAsync();
    Task<UsuarioDto?> CreateUsuarioAsync(CrearUsuarioDto dto);
    Task<bool> UpdateUsuarioAsync(int id, PutUsuarioDto dto);
    Task<bool> DeleteUsuarioAsync(int id);
    Task<bool> ResetPasswordUsuarioAsync(int id, ResetPasswordDto dto);
}

public class UsuarioService (HttpClient http) : IUsuarioService
{
    //Obtengo todos los usuarios
    public async Task<UsuarioDto[]> GetUsuariosAsync()
    {
        return await http.GetFromJsonAsync<UsuarioDto[]>("usuarios", JsonConfig.Options) ?? [];
    }

    //Crear un usuario
    public async Task<UsuarioDto?> CreateUsuarioAsync(CrearUsuarioDto dto)
    {
        //Aguardo el post de usuario y guardo la respuesta
        HttpResponseMessage response = await http.PostAsJsonAsync("usuarios", dto, JsonConfig.Options);
        //Si es exitoso, devuelvo el usuario
        if (response.IsSuccessStatusCode)
        {
            var successful = await response.Content.ReadFromJsonAsync<UsuarioDto>(JsonConfig.Options);
            return successful;
        }
        else
        {
            return null;
        }
    }

    //Editar usuario, se ejecuta directo en la ruta
    public async Task<bool> UpdateUsuarioAsync(int id, PutUsuarioDto dto)
    {
        var response = await http.PutAsJsonAsync($"usuarios/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    //Eliminar usuario, se ejecuta directo en la ruta
    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        var response = await http.DeleteAsync($"usuarios/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetPasswordUsuarioAsync(int id, ResetPasswordDto dto)
    {
        var hasChanged = await http.PutAsJsonAsync($"usuarios/{id}/password", dto);
        return hasChanged.IsSuccessStatusCode;
    }
}
