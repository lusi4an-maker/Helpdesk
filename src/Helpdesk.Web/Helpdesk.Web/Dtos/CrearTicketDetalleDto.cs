namespace Helpdesk.Web.Dtos;

public class CrearTicketDetalleDto
{
    public string Comentario { get; set; } = string.Empty;
    public bool EsInterno { get; set; } = false;
}
