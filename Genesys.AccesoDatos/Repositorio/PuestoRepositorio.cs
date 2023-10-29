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
    public class PuestoRepositorio : Repositorio<Puesto>, IPuestoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public PuestoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar (Puesto puesto)
        {
            var puestoBD = _db.Puestos.FirstOrDefault(b => b.IdPuesto == puesto.IdPuesto);
            if (puestoBD != null)
            {
                puestoBD.NombrePuesto = puesto.NombrePuesto;
                puestoBD.Descripcion = puesto.Descripcion;
                puestoBD.Sueldo = puesto.Sueldo;
                puestoBD.StatusPuesto = puesto.StatusPuesto;
                _db.SaveChanges(); //Guarda los cambios en la base de datos
            }
        }
    }
}
