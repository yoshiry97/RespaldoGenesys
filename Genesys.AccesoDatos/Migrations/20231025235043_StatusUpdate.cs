using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesys.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class StatusUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatusPuesto",
                table: "Puestos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusPlanta",
                table: "Plantas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusDocumento",
                table: "Documentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusDatosBancarios",
                table: "DatosBancarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPuesto",
                table: "Puestos");

            migrationBuilder.DropColumn(
                name: "StatusPlanta",
                table: "Plantas");

            migrationBuilder.DropColumn(
                name: "StatusDocumento",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "StatusDatosBancarios",
                table: "DatosBancarios");
        }
    }
}
