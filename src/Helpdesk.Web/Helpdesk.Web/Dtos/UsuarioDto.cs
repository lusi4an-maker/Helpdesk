using Helpdesk.Web.Models;

namespace Helpdesk.Web.Dtos;

public class UsuarioDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? NombrePila { get; set; }
    public string? ApellidoPila { get; set; }
    public RolUsuario Rol { get; set; }
    public EstadoUsuario Estado { get; set; }



}
