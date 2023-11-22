using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Auxiliar)]
    public class PuestoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public PuestoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id) //id lleva signo de pregunta porque puede ir vacio
        {
            Puesto puesto = new Puesto(); 
            if (id == null)
            {
                //Crear una nuevo empleado
                puesto.StatusPuesto = true;
                return View(puesto);
            }
            //Actualizamos empleado
            puesto = await _unidadTrabajo.Puesto.Obtener(id.GetValueOrDefault());
            if (puesto == null)
            {
                return NotFound();
            }
            return View(puesto);
        }
        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken] //Sirve para evitar las falsificaciones de solicitudes de un sitio cargado normalmente de otra pagina
        public async Task<IActionResult> Upsert(Puesto puesto)
        {
            if (ModelState.IsValid) //validamos que todas las propiedades del modelo sean validas
            {
                if (puesto.IdPuesto == 0)
                {
                    await _unidadTrabajo.Puesto.Agregar(puesto);
                    TempData[DS.Exitosa] = "Puesto Creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Puesto.Actualizar(puesto);
                    TempData[DS.Exitosa] = "Puesto Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar Puesto";
            return View(puesto); //si el modelo no es valido, hacemos un return a la misma vista y le mandamos empleado.
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Puesto.ObtenerTodos(); //El metodo obtener todos trae una lista
            return Json(new { data = todos }); //todos tiene la lista de empleados, data lo referenciaremos desde el javascript
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var puestoBD = await _unidadTrabajo.Puesto.Obtener(id);
            if (puestoBD == null)
            {
                return Json(new { success = false, message = "Error al borrar puesto" });
            }
            _unidadTrabajo.Puesto.Remover(puestoBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Puesto eliminado exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Puesto.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.NombrePuesto.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombrePuesto.ToLower().Trim() == nombre.ToLower().Trim() && b.IdPuesto != id);
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
