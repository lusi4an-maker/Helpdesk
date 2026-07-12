
using System.Net.Http.Json;
using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;

namespace Helpdesk.Web.Services;

public interface IUsuarioService
{
    Task<UsuarioDto[]> GetUsuariosAsync();
    Task<UsuarioDto?> CreateUsuarioAsync(CrearUsuarioDto dto);
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
}
