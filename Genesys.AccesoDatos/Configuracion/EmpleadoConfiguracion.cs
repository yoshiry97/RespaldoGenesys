using Genesys.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Configuracion
{
    public class EmpleadoConfiguracion : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.Property(x => x.IdEmpleado).IsRequired();
            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(60);
            builder.Property(x => x.ApPaterno).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ApMaterno).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FechaNacimiento).IsRequired();
            builder.Property(x => x.NSS).IsRequired().HasMaxLength(11);
            builder.Property(x => x.CURP).IsRequired().HasMaxLength(18);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.IdPuesto).IsRequired();
            builder.Property(x => x.FechaIngreso).IsRequired();
            builder.Property(x => x.TipoNomina).IsRequired();
            builder.Property(x => x.IdPlanta).IsRequired();
            builder.Property(x => x.Turno).IsRequired();
            builder.Property(x => x.NumeroGafete).IsRequired();
            builder.Property(x => x.StatusEmpleado).IsRequired();

        }
    }
}
