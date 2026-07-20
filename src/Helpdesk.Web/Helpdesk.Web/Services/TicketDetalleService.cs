using System.Net.Http.Json;
using Helpdesk.Web.Json;
using Helpdesk.Web.Dtos;
using Helpdesk.Web.Extensions;

namespace Helpdesk.Web.Services;

public interface ITicketDetalleService
{
    Task<TicketDetalleResponseDto[]> GetDetallesAsync(int ticketId);
    Task<ResultadoApi<TicketDetalleResponseDto>> CreateDetalleAsync(int ticketId, CrearTicketDetalleDto dto);
}
public class TicketDetalleService (HttpClient http): ITicketDetalleService
{
    public async Task<TicketDetalleResponseDto[]> GetDetallesAsync(int ticketId)
    {
        //Obtengo el detalle junto al rol mediante options
        return await http.GetFromJsonAsync<TicketDetalleResponseDto[]>($"tickets/{ticketId}/detalles", JsonConfig.Options) ?? [];
    }

    public async Task<ResultadoApi<TicketDetalleResponseDto>> CreateDetalleAsync(int ticketId, CrearTicketDetalleDto dto)
    {
        //Leemos el response del post, si es exitoso se crea el ticket, sino se tira error especificado
        HttpResponseMessage response = await http.PostAsJsonAsync($"tickets/{ticketId}/detalles", dto);
        if (response.IsSuccessStatusCode)
        {
            var succesful = await response.Content.ReadFromJsonAsync<TicketDetalleResponseDto>(JsonConfig.Options);
            return new ResultadoApi<TicketDetalleResponseDto>(true, succesful, null);
        }
        else
        {
            return new ResultadoApi<TicketDetalleResponseDto>(false, null, await response.LeerErrorAsync());
        }
    }
}
