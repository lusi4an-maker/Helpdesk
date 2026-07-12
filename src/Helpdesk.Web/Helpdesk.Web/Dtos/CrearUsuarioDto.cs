

using Helpdesk.Web.Models;

namespace Helpdesk.Web.Dtos;

public class CrearUsuarioDto
{
    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? NombrePila { get; set; }
    public string? ApellidoPila { get; set; }
    public string? Password { get; set; }
    public RolUsuario Rol { get; set; }
}
