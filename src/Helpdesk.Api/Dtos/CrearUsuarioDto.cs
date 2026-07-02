using System.ComponentModel.DataAnnotations;

using Helpdesk.Api.Models;
namespace Helpdesk.Api.Dtos;

public record CrearUsuarioDto(

    [property: Required(ErrorMessage = "Ingrese el nombre de usuario")]
    [property: StringLength(15, MinimumLength = 3)]
    string Nombre,

    [property: Required(ErrorMessage = "Ingrese un correo electronico")]
    [property: RegularExpression(@".*@.*", ErrorMessage = "El correo debe tener el simbolo '@'")]
    [property: StringLength(50, MinimumLength = 10)]
    string Email,

    [property: Required(ErrorMessage = "Ingrese el nombre pila de la persona")]
    [property: StringLength(100, MinimumLength = 3)]
    string NombrePila,

    [property: Required(ErrorMessage = "Ingrese el apellido pila de la persona")]
    [property: StringLength(100, MinimumLength = 3)]
    string ApellidoPila,

    [property: Required(ErrorMessage = "Ingrese una contraseña")]
    [property: StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe ser de al menos 8 caracteres.")]
    [property: RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe tener mayusculas, minusculas, numeros y caracteres especiales." )]
    string Password,

    [property: Required(ErrorMessage = "Ingrese un rol para el usuario.")]
    RolUsuario? Rol
);

