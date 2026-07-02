using System.ComponentModel.DataAnnotations;

using Helpdesk.Api.Models;

namespace Helpdesk.Api.Dtos;

public record ActualizarEstadoUsuarioDto(
    [property: Required(ErrorMessage ="Ingresa el estado del usuario.")]
    EstadoUsuario? EstadoUsuario
);