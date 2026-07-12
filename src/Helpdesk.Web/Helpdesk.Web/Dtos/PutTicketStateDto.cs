using Helpdesk.Web.Models;

namespace Helpdesk.Web.Dtos;

public class PutTicketStateDto
{
    public EstadoTicket EstadoTicket { get; set; }
}
