using Helpdesk.Api.Models;

namespace Helpdesk.Api.Dtos;

public record TicketDetalleResponseDto
(int Id,
    string Comentario,
    string NombreAutor,
    RolUsuario RolAutor,
    DateTimeOffset FechaCreacion,
    bool EsInterno);

