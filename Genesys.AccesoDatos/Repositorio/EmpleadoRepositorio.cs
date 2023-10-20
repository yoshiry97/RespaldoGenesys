﻿using Genesys.AccesoDatos.Data;
using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio
{
    public class EmpleadoRepositorio : Repositorio<Empleado>, IEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public EmpleadoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar (Empleado empleado)
        {
            var empleadoBD = _db.Empleados.FirstOrDefault(b => b.IdEmpleado == empleado.IdEmpleado);
            if (empleadoBD != null)
            {
                empleadoBD.Nombres = empleado.Nombres;
                empleadoBD.ApPaterno = empleado.ApPaterno;
                empleadoBD.ApMaterno = empleado.ApMaterno;
                empleadoBD.FechaNacimiento = empleado.FechaNacimiento;
                empleadoBD.NSS = empleado.NSS;
                empleadoBD.CURP = empleado.CURP;
                empleadoBD.Email = empleado.Email;
                empleadoBD.IdPuesto = empleado.IdPuesto;
                empleadoBD.FechaIngreso = empleado.FechaIngreso;
                empleadoBD.TipoNomina = empleado.TipoNomina;
                empleadoBD.IdPlanta = empleado.IdPlanta;
                empleadoBD.Turno = empleado.Turno;
                empleadoBD.NumeroGafete = empleado.NumeroGafete;
                empleadoBD.StatusEmpleado = empleado.StatusEmpleado;
                _db.SaveChanges(); //Guarda los cambios en la base de datos
            }
        }
    }
}
