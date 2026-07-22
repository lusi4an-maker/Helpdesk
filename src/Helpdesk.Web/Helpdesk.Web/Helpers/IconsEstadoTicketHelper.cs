using Helpdesk.Web.Models;

using MudBlazor;

namespace Helpdesk.Web.Helpers;

public static class IconsEstadoTicketHelper
{
    public static string GetIcons(this EstadoTicket estado) => estado switch
    {
        EstadoTicket.Abierto => Icons.Material.Filled.Inbox,
        EstadoTicket.Pendiente => Icons.Material.Filled.Pending,
        EstadoTicket.EnProgreso => Icons.Material.Filled.PendingActions,
        EstadoTicket.Cerrado => Icons.Material.Filled.TaskAlt,
        EstadoTicket.Hecho => Icons.Material.Filled.Task,
        _ => default
    };

}

