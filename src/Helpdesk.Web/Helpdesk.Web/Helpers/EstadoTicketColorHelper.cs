using Helpdesk.Web.Models;

using MudBlazor;

namespace Helpdesk.Web.Helpers;

public static class EstadoTicketColorHelper
{
    public static Color GetColor(this EstadoTicket ticket) => ticket switch
    {
        EstadoTicket.Abierto => Color.Info,
        EstadoTicket.Pendiente => Color.Default,
        EstadoTicket.EnProgreso => Color.Warning,
        EstadoTicket.Cerrado => Color.Error,
        EstadoTicket.Hecho => Color.Success,
        _ => default

    };
}
