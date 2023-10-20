using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesys.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEmpleadoMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ApPaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApMaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NSS = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPuesto = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoNomina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPlanta = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroGafete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusEmpleado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");
        }
    }
}
