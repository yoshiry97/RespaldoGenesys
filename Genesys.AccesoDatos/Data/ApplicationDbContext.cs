using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Genesys.Modelos;
using System.Reflection;

namespace Genesys.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Documentos> Documentos { get; set; }
        public DbSet<DatosBancarios> DatosBancarios { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}