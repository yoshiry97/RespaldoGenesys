﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesys.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CambiosDatosBancariosMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DatosBancarios_IdEmpleado",
                table: "DatosBancarios",
                column: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_DatosBancarios_Empleados_IdEmpleado",
                table: "DatosBancarios",
                column: "IdEmpleado",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatosBancarios_Empleados_IdEmpleado",
                table: "DatosBancarios");

            migrationBuilder.DropIndex(
                name: "IX_DatosBancarios_IdEmpleado",
                table: "DatosBancarios");
        }
    }
}
