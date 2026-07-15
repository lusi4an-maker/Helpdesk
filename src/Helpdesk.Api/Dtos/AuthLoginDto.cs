using System.ComponentModel.DataAnnotations;
namespace Helpdesk.Api.Dtos;

public record AuthLoginDto
(
    [property: Required(ErrorMessage = "Ingrese su nombre de usuario")]
    [property: StringLength(15, MinimumLength = 3)]
    string Nombre,

    [property: Required(ErrorMessage = "Ingrese una contraseña")]
    string Password
);