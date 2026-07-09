using System.Net.Http.Json;
using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;

namespace Helpdesk.Web.Services;

public interface ITicketService
{
    Task<TicketDto[]> GetTicketsAsync();
    Task<TicketDto?> CreateTicketAsync(CrearTicketDto dto);
}

public class TicketService(HttpClient http) : ITicketService
{
    public async Task<TicketDto[]> GetTicketsAsync()
    {
        return await http.GetFromJsonAsync<TicketDto[]>("tickets", JsonConfig.Options) ?? [];
    }

    public async Task<TicketDto?> CreateTicketAsync(CrearTicketDto dto)
    {
        HttpResponseMessage response = await http.PostAsJsonAsync("tickets",dto);
        if (response.IsSuccessStatusCode) 
        {
            var succesful = await response.Content.ReadFromJsonAsync<TicketDto>(JsonConfig.Options);
            return succesful;
        }
        else
        {
            return null;
        }
    }
}
