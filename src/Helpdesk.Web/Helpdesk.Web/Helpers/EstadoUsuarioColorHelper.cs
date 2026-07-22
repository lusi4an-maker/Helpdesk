using Helpdesk.Web.Models;

using MudBlazor;

namespace Helpdesk.Web.Helpers;

public static class EstadoUsuarioColorHelper
{
    public static Color GetColor(this EstadoUsuario estado) => estado switch
    {
        EstadoUsuario.Activo => Color.Success,
        EstadoUsuario.Bloqueado => Color.Error,
        EstadoUsuario.Inactivo => Color.Default,
        _ => default
    };

}
