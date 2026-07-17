using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Api.Dtos;
public record CrearTicketDetalleDto(
    [property: Required(ErrorMessage = "Ingrese una descripcion para el detalle")]
    string Descripcion,

    bool EsInterno = false
    
    );