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
    public class DocumentosConfiguracion : IEntityTypeConfiguration<Documentos>
    {
        public void Configure(EntityTypeBuilder<Documentos> builder)
        {
            builder.Property(x => x.IdDocumento).IsRequired();
            builder.Property(x => x.NombreDocumento).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Archivo).IsRequired();
            builder.Property(x => x.IdEmpleado).IsRequired();
      }
    }
}
