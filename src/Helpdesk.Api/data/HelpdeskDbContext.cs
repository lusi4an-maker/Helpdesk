using Microsoft.EntityFrameworkCore;
using Helpdesk.Api.Models;
namespace Helpdesk.Api.Data;
public class HelpdeskDbContext : DbContext
{
    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) {}
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<TicketDetalle> TicketDetalles => Set<TicketDetalle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketDetalle>()
            .HasOne(td => td.Usuario)
            .WithMany(u => u.TicketDetalles)
            .HasForeignKey(td => td.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AgenteAsignado)
            .WithMany()
            .HasForeignKey(t => t.AgenteAsignadoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}