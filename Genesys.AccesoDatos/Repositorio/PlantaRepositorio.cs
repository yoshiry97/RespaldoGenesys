using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class PlantaRepositorio : Repositorio<Planta>, IPlantaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public PlantaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar (Planta planta)
        {
            var plantaBD = _db.Plantas.FirstOrDefault(b => b.IdPlanta == planta.IdPlanta);
            if (plantaBD != null)
            {
                plantaBD.NombrePlanta = planta.NombrePlanta;
                plantaBD.StatusPlanta = planta.StatusPlanta;
                _db.SaveChanges(); //Guarda los cambios en la base de datos
            }
        }
    }
}
