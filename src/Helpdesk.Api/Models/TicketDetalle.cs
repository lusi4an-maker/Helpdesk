
namespace Helpdesk.Api.Models;

public class TicketDetalle
{
    public int Id {get; set;}
    public string Descripcion {get; set;} = "";
    public int UsuarioId {get; set;}
    public Usuario? Usuario {get; set;}
    public DateTimeOffset UltimaRevision {get; set;} = DateTimeOffset.UtcNow;
    public int TicketId {get; set;}
    public Ticket? Ticket {get; set;}
}