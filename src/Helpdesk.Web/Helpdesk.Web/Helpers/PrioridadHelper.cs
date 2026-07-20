using MudBlazor;
using Helpdesk.Web.Models;

namespace Helpdesk.Web.Helpers;

public static class PrioridadHelper
{
    public static Color GetColor(this PrioridadTicket prioridadTicket) => prioridadTicket switch
    {
        PrioridadTicket.Baja => Color.Default,
        PrioridadTicket.Media => Color.Info,
        PrioridadTicket.Alta => Color.Warning,
        PrioridadTicket.Urgente => Color.Error,
        _ => default
    };
}
