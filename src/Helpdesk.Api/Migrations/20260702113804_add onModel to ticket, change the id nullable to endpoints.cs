using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helpdesk.Api.Migrations
{
    /// <inheritdoc />
    public partial class addonModeltoticketchangetheidnullabletoendpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgenteAsignado",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "AgenteAsignadoId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AgenteAsignadoId",
                table: "Tickets",
                column: "AgenteAsignadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Usuarios_AgenteAsignadoId",
                table: "Tickets",
                column: "AgenteAsignadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Usuarios_AgenteAsignadoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AgenteAsignadoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AgenteAsignadoId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "AgenteAsignado",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
