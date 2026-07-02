using System.ComponentModel.DataAnnotations;

using Helpdesk.Api.Models;

namespace Helpdesk.Api.Dtos;

public record ActualizarEstadoTicketDto(
    [property: Required(ErrorMessage ="Ingresa el estado del ticket.")]
    EstadoTicket? EstadoTicket
);