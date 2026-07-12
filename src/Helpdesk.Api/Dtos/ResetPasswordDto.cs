using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Api.Dtos;

public record ResetPasswordDto
(
    [property: Required(ErrorMessage = "Ingrese una contraseña")]
    [property: StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe ser de al menos 8 caracteres.")]
    [property: RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe tener mayusculas, minusculas, numeros y caracteres especiales.")]
    string NewPassword
);