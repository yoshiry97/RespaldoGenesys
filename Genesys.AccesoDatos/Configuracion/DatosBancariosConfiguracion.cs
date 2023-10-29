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
    internal class DatosBancariosConfiguracion : IEntityTypeConfiguration<DatosBancarios>
    {
        public void Configure(EntityTypeBuilder<DatosBancarios> builder)
        {
            builder.Property(x => x.IdDatosBancarios).IsRequired();
            builder.Property(x => x.NombreBanco).IsRequired().HasMaxLength(50);
            builder.Property(x => x.NumeroCuenta).IsRequired().HasMaxLength(20);
            builder.Property(x => x.ClabeInterbancaria).IsRequired().HasMaxLength(18);
            builder.Property(x => x.Prestamos);
            builder.Property(x => x.IdEmpleado).IsRequired();
            builder.Property(x => x.StatusDatosBancarios).IsRequired();
        }
    }
}
