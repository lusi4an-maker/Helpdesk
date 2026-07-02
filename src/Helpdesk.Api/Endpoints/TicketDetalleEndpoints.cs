using Microsoft.EntityFrameworkCore;
using Helpdesk.Api.Data;
using Helpdesk.Api.Dtos;
using Helpdesk.Api.Models;
using System.Security.Claims;

namespace Helpdesk.Api.Endpoints;

public static class TicketDetalleEndpoints
{
    public static IEndpointRouteBuilder MapTicketDetalleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tickets/{ticketId}/detalles")
                        .WithTags("TicketDetalles")
                        .RequireAuthorization();

        group.MapGet("/", GetDetalles);
        group.MapPost("/", PostDetalle)
            .RequireAuthorization("SoloPersonal");

        return app;
    }

    //Todos los detalles de un ticket si existen.
    private static async Task<IResult> GetDetalles(int ticketId, HelpdeskDbContext contexto, HttpContext http)
    {
        var ticket = await contexto.Tickets.FindAsync(ticketId);
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var rol = http.User.FindFirstValue(ClaimTypes.Role);
        
        //Verificamos si hay ticket y usuario autorizados
        if (ticket is null)
        {
            return Results.NotFound();
        }
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        
        var usuarioInt = int.Parse(usuario); //Parse para el id usuario
        bool puedeVer = 
            //Es admin o gerente:
            rol == "Administrador" || rol == "Gerente" ||
            //Es cliente y es su propio ticket
            (rol == "Cliente" && ticket.UsuarioCreo == usuarioInt) ||
            //Es agente o analista y tiene asignado el ticket
            ((rol == "Agente" || rol == "Analista") && ticket.AgenteAsignadoId == usuarioInt);

        //Si no puede ver el detalle, forbid
        if (!puedeVer)
        {
            return Results.Forbid();
        }
        var ticketDetalle = await contexto.TicketDetalles.Where(d => d.TicketId == ticketId).ToListAsync();
        return Results.Ok(ticketDetalle);
    }

    //Crear un detalle
    private static async Task<IResult> PostDetalle(int ticketId, CrearTicketDetalleDto dto, HttpContext http, HelpdeskDbContext contexto)
    {
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        
        TicketDetalle nuevo = new TicketDetalle
        {
            TicketId = ticketId,
            Descripcion = dto.Descripcion,
            UsuarioId = int.Parse(usuario)
        };
        contexto.TicketDetalles.Add(nuevo);
        await contexto.SaveChangesAsync();
        return Results.Created($"/tickets/{ticketId}/detalles/{nuevo.Id}", nuevo);
    }
}