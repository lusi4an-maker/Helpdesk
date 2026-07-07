using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;
using System.Net.Http.Json;

namespace Helpdesk.Web.Services;

public interface ITicketService
{
    Task<TicketDto[]> GetTicketsAsync();
}

public class TicketService(HttpClient http) : ITicketService
{
    public async Task<TicketDto[]> GetTicketsAsync()
    {
        return await http.GetFromJsonAsync<TicketDto[]>("tickets", JsonConfig.Options) ?? [];
    }
}
