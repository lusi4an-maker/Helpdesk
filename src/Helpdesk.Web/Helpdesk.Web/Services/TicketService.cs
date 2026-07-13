using System.Net.Http.Json;
using Helpdesk.Web.Dtos;
using Helpdesk.Web.Json;

namespace Helpdesk.Web.Services;

public interface ITicketService
{
    Task<TicketDto[]> GetTicketsAsync();
    Task<TicketDto?> CreateTicketAsync(CrearTicketDto dto);
    Task<bool> DeleteTicketAsync(int id);
    Task<TicketDto?> UpdateTicketAsync(int id, PutTicketDto dto);
    Task<bool> ChangeTicketStateAsync(int id, PutTicketStateDto dto);
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

    public async Task<bool> DeleteTicketAsync(int id)
    {
        var response = await http.DeleteAsync($"tickets/{id}");
        return response.IsSuccessStatusCode;
    }

    //Actualizar ticket
    public async Task<TicketDto?> UpdateTicketAsync(int id, PutTicketDto dto)
    {
        var response = await http.PutAsJsonAsync($"tickets/{id}", dto);
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

    public async Task<bool> ChangeTicketStateAsync(int id, PutTicketStateDto dto)
    {
        var message = await http.PutAsJsonAsync($"tickets/{id}/status", dto,JsonConfig.Options);
        return message.IsSuccessStatusCode;
    }
}
