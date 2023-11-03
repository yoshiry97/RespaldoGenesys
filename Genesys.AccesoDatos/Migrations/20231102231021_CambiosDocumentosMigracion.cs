using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesys.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CambiosDocumentosMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Documentos");

            migrationBuilder.AddColumn<string>(
                name: "ArchivoUrl",
                table: "Documentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdEmpleado",
                table: "Documentos",
                column: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Empleados_IdEmpleado",
                table: "Documentos",
                column: "IdEmpleado",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Empleados_IdEmpleado",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_IdEmpleado",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ArchivoUrl",
                table: "Documentos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Documentos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
