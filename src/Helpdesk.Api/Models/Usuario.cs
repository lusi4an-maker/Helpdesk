namespace Helpdesk.Api.Models;

public class Usuario
{
    public int Id {get; set;}
    public string Nombre {get; set;} = "";
    public string Email {get; set;} = "";
    public string PasswordHash {get; set;} = "";
    public string NombrePila {get; set;} = "";
    public string ApellidoPila {get; set;} = "";
    public bool DebeCambiarCredenciales {get; set;} = true;

    public List<Ticket> Tickets {get; set;} = new();
    public List<TicketDetalle> TicketDetalles {get; set;} = new();
    public EstadoUsuario Estado {get; set;} = EstadoUsuario.Activo;
    public RolUsuario Rol {get; set;} = RolUsuario.Cliente;
}