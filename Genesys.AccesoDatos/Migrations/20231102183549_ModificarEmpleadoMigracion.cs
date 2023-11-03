using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesys.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModificarEmpleadoMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPlanta",
                table: "Empleados",
                column: "IdPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPuesto",
                table: "Empleados",
                column: "IdPuesto");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Plantas_IdPlanta",
                table: "Empleados",
                column: "IdPlanta",
                principalTable: "Plantas",
                principalColumn: "IdPlanta");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Puestos_IdPuesto",
                table: "Empleados",
                column: "IdPuesto",
                principalTable: "Puestos",
                principalColumn: "IdPuesto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Plantas_IdPlanta",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Puestos_IdPuesto",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_IdPlanta",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_IdPuesto",
                table: "Empleados");
        }
    }
}
