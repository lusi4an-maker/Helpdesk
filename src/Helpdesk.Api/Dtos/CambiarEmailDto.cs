using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Api.Dtos;

public record CambiarEmailDto(

    [property: Required(ErrorMessage = "Ingrese un correo electronico")]
    [property: RegularExpression(@".*@.*", ErrorMessage = "El correo debe tener el simbolo '@'")]
    [property: StringLength(50, MinimumLength = 10)]
    string NewEmail
);

