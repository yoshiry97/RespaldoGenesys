﻿using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Auxiliar + "," + DS.Role_Gerente)]
    public class PlantaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public PlantaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id) //id lleva signo de pregunta porque puede ir vacio
        {
            Planta planta = new Planta();
            if (id == null)
            {
                //Crear una nuevo empleado
                planta.StatusPlanta = true;
                return View(planta);
            }
            //Actualizamos empleado
            planta = await _unidadTrabajo.Planta.Obtener(id.GetValueOrDefault());
            if (planta == null)
            {
                return NotFound();
            }
            return View(planta);
        }
        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken] //Sirve para evitar las falsificaciones de solicitudes de un sitio cargado normalmente de otra pagina
        public async Task<IActionResult> Upsert(Planta planta)
        {
            if (ModelState.IsValid) //validamos que todas las propiedades del modelo sean validas
            {
                if (planta.IdPlanta == 0)
                {
                    await _unidadTrabajo.Planta.Agregar(planta);
                    TempData[DS.Exitosa] = "Planta Creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Planta.Actualizar(planta);
                    TempData[DS.Exitosa] = "Empleado Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar Empleado";
            return View(planta); //si el modelo no es valido, hacemos un return a la misma vista y le mandamos empleado.
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Planta.ObtenerTodos(filtro: x => x.StatusPlanta == true); //El metodo obtener todos trae una lista
            return Json(new { data = todos }); //todos tiene la lista de empleados, data lo referenciaremos desde el javascript
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var plantaBD = await _unidadTrabajo.Planta.Obtener(id);
            if (plantaBD == null)
            {
                return Json(new { success = false, message = "Error al borrar planta" });
            }
            plantaBD.StatusPlanta = false;
            _unidadTrabajo.Planta.Actualizar(plantaBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Planta eliminada exitosamente" });
        }
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Planta.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.NombrePlanta.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombrePlanta.ToLower().Trim() == nombre.ToLower().Trim() && b.IdPlanta != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }

        #endregion
    }
}