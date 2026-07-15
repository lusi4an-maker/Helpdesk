using Microsoft.EntityFrameworkCore;
using Helpdesk.Api.Data;
using Helpdesk.Api.Dtos;
using Helpdesk.Api.Models;
using System.Security.Claims;

namespace Helpdesk.Api.Endpoints;

public static class TicketEndpoints
{
    public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tickets").WithTags("Tickets").RequireAuthorization();

        group.MapGet("/", GetTickets);
        group.MapGet("/{id}", GetTicketId);
        group.MapPost("/", PostTicket);
        group.MapPut("/{id}", PutTicket);
        group.MapDelete("/{id}", DeleteTicket)
            .RequireAuthorization("SoloAdmins");
        group.MapPut("/{ticketId}/assign", AssignTicket)
            .RequireAuthorization("SoloAdmins");
        group.MapPut("/{ticketId}/status", StatusTicket)
            .RequireAuthorization("SoloPersonal");

        return app;
    }

    //Get TODOS los tickets
    private static async Task<IResult> GetTickets(HelpdeskDbContext contexto, HttpContext http)
    {
        var rol = http.User.FindFirstValue(ClaimTypes.Role);
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = contexto.Tickets.AsQueryable();

        if (usuario is null)
        {
            return Results.Unauthorized();
        }

        //Si es cliente, solo ve sus propios tickets, si es agente/analista, solo los que se les asigno. Admin y Gerencia ve todos.
        if (rol == "Cliente")
        {
            query = query.Where(t => t.UsuarioCreo == int.Parse(usuario));
        }
        else if (rol == "Agente" || rol == "Analista")
        {
            query = query.Where(t => t.AgenteAsignadoId == int.Parse(usuario));
        }
        return Results.Ok(await query.Select(t => new TicketResponseDto(
                                                        t.Id, 
                                                        t.Titulo, 
                                                        t.Descripcion, 
                                                        t.FechaCreacion, 
                                                        t.Estado,
                                                        t.Usuario.NombrePila + " " + t.Usuario.ApellidoPila,
                                                        t.AgenteAsignado == null ? null : t.AgenteAsignado.NombrePila + " " + t.AgenteAsignado.ApellidoPila,
                                                        t.UsuarioCreo,
                                                        t.AgenteAsignadoId)).ToListAsync());
    }

    //Get ticket por ID
    private static async Task<IResult> GetTicketId(int id, HelpdeskDbContext contexto, HttpContext http)
    {
        var ticket = await contexto.Tickets
            .Where(t => t.Id == id)
            .Select(t => new TicketResponseDto(
                t.Id,
                t.Titulo,
                t.Descripcion, 
                t.FechaCreacion,
                t.Estado,
                t.Usuario.NombrePila + " " + t.Usuario.ApellidoPila,
                t.AgenteAsignado == null ? null : t.AgenteAsignado.NombrePila + " " + t.AgenteAsignado.ApellidoPila,
                t.UsuarioCreo,
                t.AgenteAsignadoId
                )).FirstOrDefaultAsync();
        var rol = http.User.FindFirstValue(ClaimTypes.Role);
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (ticket is null)
        {
            return Results.NotFound();
        }
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        //usuario se crea como string, parseo a int
        var usuarioInt = int.Parse(usuario);
        //Si es cliente y el id de usuario es distinto al que lo creo, forbid
        if (rol == "Cliente" && ticket.UsuarioCreo != usuarioInt)
        {
            return Results.Forbid();
        } 
        //Si es agente o analista y el ticket no es el que tiene asignado, forbid
        else if ((rol == "Agente" || rol == "Analista") && ticket.AgenteAsignadoId != usuarioInt)
        {
            return Results.Forbid();
        }

        return Results.Ok(ticket);
    }

    //Crear ticket
    private static async Task<IResult> PostTicket(CrearTicketDto dto, HelpdeskDbContext contexto, HttpContext http)
    {
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        
        
        Ticket nuevo = new Ticket
        {
            UsuarioCreo = int.Parse(usuario),
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion
        };

        contexto.Tickets.Add(nuevo);
        await contexto.SaveChangesAsync();
        return Results.Created($"/tickets/{nuevo.Id}", await contexto.Tickets
            .Where(t => t.Id == nuevo.Id)
            .Select(t => new TicketResponseDto(
                t.Id,
                t.Titulo,
                t.Descripcion,
                t.FechaCreacion,
                t.Estado,
                t.Usuario.NombrePila + " " + t.Usuario.ApellidoPila,
                t.AgenteAsignado == null ? null : t.AgenteAsignado.NombrePila + " " + t.AgenteAsignado.ApellidoPila,
                t.UsuarioCreo,
                t.AgenteAsignadoId
                )).FirstAsync());
    }

    //Modificar ticket (solo titulo y descripcion)
    private static async Task<IResult> PutTicket(int id, ActualizarTicketDto dto, HelpdeskDbContext contexto, HttpContext http)
    {
        var ticket = await contexto.Tickets.FindAsync(id);
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var rol = http.User.FindFirstValue(ClaimTypes.Role);
        
        if (ticket is null)
        {
            return Results.NotFound();
        }
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        
        var usuarioInt = int.Parse(usuario); //Parse para el id usuario
        bool puedeEditar = 
            //Es admin o gerente:
            rol == "Administrador" || rol == "Gerente" ||
            //Es cliente y es su propio ticket
            (rol == "Cliente" && ticket.UsuarioCreo == usuarioInt) ||
            //Es agente o analista y tiene asignado el ticket
            ((rol == "Agente" || rol == "Analista") && ticket.AgenteAsignadoId == usuarioInt);

        //Si no puede editar, forbid
        if (!puedeEditar)
        {
            return Results.Forbid();
        }

        ticket.Titulo = dto.Titulo;
        ticket.Descripcion = dto.Descripcion;

        await contexto.SaveChangesAsync();
        return Results.NoContent();
    }

    //Borrar un ticket
    private static async Task<IResult> DeleteTicket(int id, HelpdeskDbContext contexto)
    {
        var ticket = await contexto.Tickets.FindAsync(id);
        if (ticket is null)
        {
            return Results.NotFound();
        }
        contexto.Tickets.Remove(ticket);
        await contexto.SaveChangesAsync();
        return Results.NoContent();
    }

    //Asignar agente/analista a un ticket
    private static async Task<IResult> AssignTicket(int ticketId, HelpdeskDbContext contexto, AsignarTicketDto dto)
    {
        //Verifico si el ticket existe
        var ticket = await contexto.Tickets.FindAsync(ticketId);
        if (ticket is null)
        {
            return Results.NotFound();
        }

        //si el agente es null, desasignamos.
        if (dto.AgenteId is null)
        {
            ticket.AgenteAsignadoId = null;
            await contexto.SaveChangesAsync();
            return Results.NoContent();
        }

        //Verifico si el agente existe
        var agente = await contexto.Usuarios.FindAsync(dto.AgenteId);
        if (agente is null)
        {
            return Results.BadRequest("No se encontro el usuario a asignar");
        }

        //verifico si es cliente, sino no se puede asignar
        if (agente.Rol == RolUsuario.Agente || agente.Rol == RolUsuario.Analista)
        {
            ticket.AgenteAsignadoId = dto.AgenteId;
            await contexto.SaveChangesAsync();
            return Results.NoContent();
        }
        else
        {
            return Results.BadRequest("Solo puede asignar a un Agente o Analista.");
        }
    }

    //Cambiar de estados
    private static async Task<IResult> StatusTicket(int ticketId, HelpdeskDbContext contexto, ActualizarEstadoTicketDto dto, HttpContext http)
    {
        var rol = http.User.FindFirstValue(ClaimTypes.Role);
        //Verifico si el usuario a chequear existe
        var usuario = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (usuario is null)
        {
            return Results.Unauthorized();
        }
        //Parse a int
        var usuarioInt = int.Parse(usuario);
        //Verifico si el ticket existe
        var ticket = await contexto.Tickets.FindAsync(ticketId);
        if (ticket is null)
        {
            return Results.NotFound();
        }
        bool puedeEditar = 
            //Es admin o gerente:
            rol == "Administrador" || rol == "Gerente" ||
            //Es agente o analista y tiene asignado el ticket
            ((rol == "Agente" || rol == "Analista") && ticket.AgenteAsignadoId == usuarioInt);

        //Si no puede editar, forbid
        if (!puedeEditar)
        {
            return Results.Forbid();
        }
        //Verifico si tiene Estado
        if (dto.EstadoTicket is null)
        {
            return Results.BadRequest("Ingrese un estado para el ticket.");
        }
        ticket.Estado = dto.EstadoTicket.Value;

        await contexto.SaveChangesAsync();
        return Results.NoContent();
    }
}
