using System.Net.Http.Json;

using Helpdesk.Web.Dtos;
using Helpdesk.Web.Extensions;
using Helpdesk.Web.Json;

namespace Helpdesk.Web.Services;

public interface ITicketService
{
    Task<TicketDto[]> GetTicketsAsync();
    Task<ResultadoApi<TicketDto>> CreateTicketAsync(CrearTicketDto dto);
    Task<bool> DeleteTicketAsync(int id);
    Task<ResultadoApi<TicketDto>> UpdateTicketAsync(int id, PutTicketDto dto);
    Task<bool> ChangeTicketStateAsync(int id, PutTicketStateDto dto);
    Task<ResultadoApi> AssignTicketAsync(int ticketId, AsignarTicketDto dto);
}

public class TicketService(HttpClient http) : ITicketService
{
    public async Task<TicketDto[]> GetTicketsAsync()
    {
        return await http.GetFromJsonAsync<TicketDto[]>("tickets", JsonConfig.Options) ?? [];
    }

    public async Task<ResultadoApi<TicketDto>> CreateTicketAsync(CrearTicketDto dto)
    {
        HttpResponseMessage response = await http.PostAsJsonAsync("tickets",dto);
        if (response.IsSuccessStatusCode) 
        {
            var succesful = await response.Content.ReadFromJsonAsync<TicketDto>(JsonConfig.Options);
            return new ResultadoApi<TicketDto>(true, succesful, null);
        }
        else
        {
            return new ResultadoApi<TicketDto>(false, null, await response.LeerErrorAsync());
        }
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
        var response = await http.DeleteAsync($"tickets/{id}");
        return response.IsSuccessStatusCode;
    }

    //Actualizar ticket
    public async Task<ResultadoApi<TicketDto>> UpdateTicketAsync(int id, PutTicketDto dto)
    {
        var response = await http.PutAsJsonAsync($"tickets/{id}", dto);
        if (response.IsSuccessStatusCode)
        {
            var succesful = await response.Content.ReadFromJsonAsync<TicketDto>(JsonConfig.Options);
            return new ResultadoApi<TicketDto>(true, succesful, null);
        }
        else
        {
            return new ResultadoApi<TicketDto>(false, null, await response.LeerErrorAsync());
        }
    }

    public async Task<bool> ChangeTicketStateAsync(int id, PutTicketStateDto dto)
    {
        var message = await http.PutAsJsonAsync($"tickets/{id}/status", dto,JsonConfig.Options);
        return message.IsSuccessStatusCode;
    }

    public async Task<ResultadoApi> AssignTicketAsync(int ticketId, AsignarTicketDto dto)
    {
        var message = await http.PutAsJsonAsync($"tickets/{ticketId}/assign", dto);
        if (message.IsSuccessStatusCode)
        {
            return new ResultadoApi(true, null);
        }
        else
        {
            return new ResultadoApi(false, await message.LeerErrorAsync());
        }
    }
}
