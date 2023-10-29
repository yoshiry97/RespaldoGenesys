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
    public class PlantaConfiguracion : IEntityTypeConfiguration<Planta>
    {
        public void Configure(EntityTypeBuilder<Planta> builder)
        {
            builder.Property(x => x.IdPlanta).IsRequired();
            builder.Property(x => x.NombrePlanta).IsRequired().HasMaxLength(30);
        }
    }
}
