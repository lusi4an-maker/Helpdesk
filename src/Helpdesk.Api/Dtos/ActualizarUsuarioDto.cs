using System.ComponentModel.DataAnnotations;
namespace Helpdesk.Api.Dtos;

public record ActualizarUsuarioDto(

    [property: Required(ErrorMessage = "Ingrese el nombre de usuario")]
    [property: StringLength(15, MinimumLength = 3)]
    string Nombre,

    [property: Required(ErrorMessage = "Ingrese el nombre pila de la persona")]
    [property: StringLength(100, MinimumLength = 3)]
    string NombrePila,

    [property: Required(ErrorMessage = "Ingrese el apellido pila de la persona")]
    [property: StringLength(100, MinimumLength = 3)]
    string ApellidoPila

);

