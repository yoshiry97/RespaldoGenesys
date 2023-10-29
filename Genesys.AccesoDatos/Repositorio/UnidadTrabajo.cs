using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
        
    {
        private readonly ApplicationDbContext _db;
        public IEmpleadoRepositorio Empleado { get; private set; }
        public IPlantaRepositorio Planta { get; private set; }
        public IPuestoRepositorio Puesto { get; private set; }
        public IDocumentosRepositorio Documentos { get; private set; }
        public IDatosBancariosRepositorio DatosBancarios { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Empleado = new EmpleadoRepositorio(_db);
            Planta = new PlantaRepositorio(_db);
            Puesto = new PuestoRepositorio(_db);
            Documentos = new DocumentosRepositorio(_db);
            DatosBancarios = new DatosBancariosRepositorio(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
