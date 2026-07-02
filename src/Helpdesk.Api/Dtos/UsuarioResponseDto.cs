using System.ComponentModel.DataAnnotations;

using Helpdesk.Api.Models;
namespace Helpdesk.Api.Dtos;

public record UsuarioResponseDto(
    int Id,
    string Nombre,
    string Email,
    string NombrePila,
    string ApellidoPila,
    RolUsuario Rol,
    EstadoUsuario Estado
);

