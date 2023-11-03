using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Modelos.ViewModels;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmpleadoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public EmpleadoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            EmpleadoVM empleadoVM = new EmpleadoVM()
            {
                Empleado = new Empleado(),
                PlantaLista = _unidadTrabajo.Empleado.ObtenerTodosDropdownLista("Planta"),
                PuestoLista = _unidadTrabajo.Empleado.ObtenerTodosDropdownLista("Puesto")
            };
            if (id == null)
            {
                //Crear un nuevo producto
                return View(empleadoVM);
            }
            else
            {
                empleadoVM.Empleado = await _unidadTrabajo.Empleado.Obtener(id.GetValueOrDefault());
                if (empleadoVM.Empleado == null)
                {
                    return NotFound();
                }
                return View(empleadoVM);
            }
        }
        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken] //Sirve para evitar las falsificaciones de solicitudes de un sitio cargado normalmente de otra pagina
        public async Task<IActionResult> Upsert(Empleado empleado)
        {
            if (ModelState.IsValid) //validamos que todas las propiedades del modelo sean validas
            {
                if (empleado.IdEmpleado == 0)
                {
                    await _unidadTrabajo.Empleado.Agregar(empleado) ;
                    TempData[DS.Exitosa] = "Empleado Creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Empleado.Actualizar(empleado);
                    TempData[DS.Exitosa] = "Empleado Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al Grabar Empleado";
            return View(empleado); //si el modelo no es valido, hacemos un return a la misma vista y le mandamos empleado.
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Empleado.ObtenerTodos(incluirPropiedades:"Puesto,Planta"); //El metodo obtener todos trae una lista
            return Json(new { data = todos }); //todos tiene la lista de empleados, data lo referenciaremos desde el javascript
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var empleadoDb = await _unidadTrabajo.Empleado.Obtener(id);
            if (empleadoDb == null)
            {
                return Json(new { success = false, message = "Error al borrar empleado" });
            }
            _unidadTrabajo.Empleado.Remover(empleadoDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Empleado eliminado exitosamente" });
        }
       
        #endregion
    }

}
