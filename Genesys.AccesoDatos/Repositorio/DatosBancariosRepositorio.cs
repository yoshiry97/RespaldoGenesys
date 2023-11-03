using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class DatosBancariosRepositorio : Repositorio<DatosBancarios>, IDatosBancariosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DatosBancariosRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar (DatosBancarios datosBancarios)
        {
            var datosBancariosBD = _db.DatosBancarios.FirstOrDefault(b => b.IdDatosBancarios == datosBancarios.IdDatosBancarios);
            if (datosBancariosBD != null)
            {
                datosBancariosBD.NombreBanco = datosBancarios.NombreBanco;
                datosBancariosBD.NumeroCuenta = datosBancarios.NumeroCuenta;
                datosBancariosBD.ClabeInterbancaria = datosBancarios.ClabeInterbancaria;
                datosBancariosBD.Prestamos = datosBancarios.Prestamos;
                datosBancariosBD.IdEmpleado = datosBancarios.IdEmpleado;
                datosBancariosBD.StatusDatosBancarios = datosBancarios.StatusDatosBancarios;
                _db.SaveChanges(); //Guarda los cambios en la base de datos
            }
        }
        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
            if (obj == "Empleado")
            {
                return _db.Empleados
                    .Where(c => c.StatusEmpleado == true)
                    .Select(c => new SelectListItem
                    {
                        Text = $"{c.Nombres} {c.ApPaterno} {c.ApMaterno}",
                        Value = c.IdEmpleado.ToString(),
                    });
            }
            else
            {
                return null;
            }
        }
    }
}
