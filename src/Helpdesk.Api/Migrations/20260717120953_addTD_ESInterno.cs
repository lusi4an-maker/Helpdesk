using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helpdesk.Api.Migrations
{
    /// <inheritdoc />
    public partial class addTD_ESInterno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "TicketDetalles",
                newName: "Comentario");

            migrationBuilder.AddColumn<bool>(
                name: "EsInterno",
                table: "TicketDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsInterno",
                table: "TicketDetalles");

            migrationBuilder.RenameColumn(
                name: "Comentario",
                table: "TicketDetalles",
                newName: "Descripcion");
        }
    }
}
