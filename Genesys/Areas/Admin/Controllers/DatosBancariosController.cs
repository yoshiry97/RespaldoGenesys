using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DatosBancariosController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public DatosBancariosController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id) //id lleva signo de pregunta porque puede ir vacio
        {
            DatosBancarios datosBancarios = new DatosBancarios();
            if (id == null)
            {
                //Crear una nuevo empleado
                datosBancarios.StatusDatosBancarios = true;
                return View(datosBancarios);
            }
            //Actualizamos empleado
            datosBancarios = await _unidadTrabajo.DatosBancarios.Obtener(id.GetValueOrDefault());
            if (datosBancarios == null)
            {
                return NotFound();
            }
            return View(datosBancarios);
        }
        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken] //Sirve para evitar las falsificaciones de solicitudes de un sitio cargado normalmente de otra pagina
        public async Task<IActionResult> Upsert(DatosBancarios datosBancarios)
        {
            if (ModelState.IsValid) //validamos que todas las propiedades del modelo sean validas
            {
                if (datosBancarios.IdDatosBancarios == 0)
                {
                    await _unidadTrabajo.DatosBancarios.Agregar(datosBancarios);
                    TempData[DS.Exitosa] = "Datos Bancarios Creados Exitosamente";
                }
                else
                {
                    _unidadTrabajo.DatosBancarios.Actualizar(datosBancarios);
                    TempData[DS.Exitosa] = "Datos Bancarios Actualizados Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar Datos Bancarios";
            return View(datosBancarios); //si el modelo no es valido, hacemos un return a la misma vista y le mandamos empleado.
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.DatosBancarios.ObtenerTodos(); //El metodo obtener todos trae una lista
            return Json(new { data = todos }); //todos tiene la lista de datos bancarios, data lo referenciaremos desde el javascript
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var datosBancariosBD = await _unidadTrabajo.DatosBancarios.Obtener(id);
            if (datosBancariosBD == null)
            {
                return Json(new { success = false, message = "Error al borrar datos bancarios" });
            }
            _unidadTrabajo.DatosBancarios.Remover(datosBancariosBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Datos bancarios eliminados exitosamente" });
        }
        
        #endregion
    }
}
