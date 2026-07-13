using System.ComponentModel.DataAnnotations;

using Helpdesk.Api.Models;

namespace Helpdesk.Api.Dtos;

public record ActualizarRolDto
{
    [property: Required(ErrorMessage = "Ingrese el rol nuevo del usuario")]
    public RolUsuario? RolUsuario { get; set; }
}
