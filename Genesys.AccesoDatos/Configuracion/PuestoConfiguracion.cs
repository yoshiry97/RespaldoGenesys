using Genesys.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Configuracion
{
    public class PuestoConfiguracion : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {

            builder.Property(x => x.IdPuesto).IsRequired();
            builder.Property(x => x.NombrePuesto).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Sueldo).IsRequired().HasColumnType("decimal(18,2)");

        }
    }
}
