using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Modelos.ViewModels;
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
            DatosBancariosVM datosBancariosVM = new DatosBancariosVM()
            {
                datosBancarios = new DatosBancarios(),
                EmpleadoLista = _unidadTrabajo.DatosBancarios.ObtenerTodosDropdownLista("Empleado"),
            };
            if (id == null)
            {
                //Crear una nuevo empleado
                datosBancariosVM.datosBancarios.StatusDatosBancarios = true;
                return View(datosBancariosVM);
            }
            else
            {
                datosBancariosVM.datosBancarios = await _unidadTrabajo.DatosBancarios.Obtener(id.GetValueOrDefault());
                if (datosBancariosVM.datosBancarios == null)
                {
                    return NotFound();
                }
                return View(datosBancariosVM);
            }
        }


        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DatosBancariosVM datosBancariosVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (datosBancariosVM.datosBancarios.IdDatosBancarios == 0)
                    {
                        await _unidadTrabajo.DatosBancarios.Agregar(datosBancariosVM.datosBancarios);
                        TempData[DS.Exitosa] = "Datos Bancarios Creados Exitosamente";
                    }
                    else
                    {
                        _unidadTrabajo.DatosBancarios.Actualizar(datosBancariosVM.datosBancarios);
                        TempData[DS.Exitosa] = "Datos Bancarios Actualizados Exitosamente";
                    }
                    await _unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Aquí puedes registrar el error en algún registro o mostrarlo en la consola para depuración.
                    Console.WriteLine($"Error: {ex.Message}");
                    TempData[DS.Error] = "Error al procesar la solicitud";
                }
            }
            else
            {
                // Aquí puedes obtener detalles sobre los errores de validación.
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                Console.WriteLine("Errores de validación: " + string.Join(", ", errors));
            }

            TempData[DS.Error] = "Error al Grabar Datos Bancarios";
            datosBancariosVM.EmpleadoLista = _unidadTrabajo.DatosBancarios.ObtenerTodosDropdownLista("Empleado");
            return View(datosBancariosVM);
        }


        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.DatosBancarios.ObtenerTodos(incluirPropiedades: "Empleado"); //El metodo obtener todos trae una lista
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
