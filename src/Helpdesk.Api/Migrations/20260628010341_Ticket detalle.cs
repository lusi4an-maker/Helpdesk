using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helpdesk.Api.Migrations
{
    /// <inheritdoc />
    public partial class Ticketdetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalle_Tickets_TicketId",
                table: "TicketDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalle_Usuarios_UsuarioId",
                table: "TicketDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketDetalle",
                table: "TicketDetalle");

            migrationBuilder.RenameTable(
                name: "TicketDetalle",
                newName: "TicketDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalle_UsuarioId",
                table: "TicketDetalles",
                newName: "IX_TicketDetalles_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalle_TicketId",
                table: "TicketDetalles",
                newName: "IX_TicketDetalles_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketDetalles",
                table: "TicketDetalles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalles_Tickets_TicketId",
                table: "TicketDetalles",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalles_Usuarios_UsuarioId",
                table: "TicketDetalles",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalles_Tickets_TicketId",
                table: "TicketDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalles_Usuarios_UsuarioId",
                table: "TicketDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketDetalles",
                table: "TicketDetalles");

            migrationBuilder.RenameTable(
                name: "TicketDetalles",
                newName: "TicketDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalles_UsuarioId",
                table: "TicketDetalle",
                newName: "IX_TicketDetalle_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalles_TicketId",
                table: "TicketDetalle",
                newName: "IX_TicketDetalle_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketDetalle",
                table: "TicketDetalle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalle_Tickets_TicketId",
                table: "TicketDetalle",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalle_Usuarios_UsuarioId",
                table: "TicketDetalle",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
