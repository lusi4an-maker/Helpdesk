namespace Helpdesk.Web.Dtos;

public record TicketResponseDto
(
    TicketDto? Ticket,
    EstadoResponse Estado
    );

public enum EstadoResponse
{
    Ok,
    NoEncontrado,
    Prohibido
}
