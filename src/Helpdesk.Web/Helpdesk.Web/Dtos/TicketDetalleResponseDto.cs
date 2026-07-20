using Helpdesk.Web.Models;

namespace Helpdesk.Web.Dtos;

public class TicketDetalleResponseDto
{
    public int Id { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string NombreAutor {  get; set; } = string.Empty;
    public RolUsuario RolAutor { get; set; }
    public DateTimeOffset FechaCreacion { get; set; }
    public bool EsInterno { get; set; }
}
