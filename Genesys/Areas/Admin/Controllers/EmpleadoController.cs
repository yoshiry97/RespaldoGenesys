using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Modelos.ViewModels;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Auxiliar + "," + DS.Role_Gerente)]

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
                //Crear un nuevo empleado
                empleadoVM.Empleado.StatusEmpleado = true;
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
        public async Task<IActionResult> Upsert(EmpleadoVM empleadoVM)
        {
            if (ModelState.IsValid) //validamos que todas las propiedades del modelo sean validas
            {
                if (empleadoVM.Empleado.IdEmpleado == 0)
                {
                    
                    await _unidadTrabajo.Empleado.Agregar(empleadoVM.Empleado);
                    TempData[DS.Exitosa] = "Empleado Creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Empleado.Actualizar(empleadoVM.Empleado);
                    TempData[DS.Exitosa] = "Empleado Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction("Index");
            }

            empleadoVM.PuestoLista = _unidadTrabajo.Empleado.ObtenerTodosDropdownLista("Puesto");
            empleadoVM.PlantaLista = _unidadTrabajo.Empleado.ObtenerTodosDropdownLista("Planta");
            TempData[DS.Error] = "Error al Grabar Empleado";
            return View(empleadoVM); //si el modelo no es valido, hacemos un return a la misma vista y le mandamos empleado.
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            
            var todos = await _unidadTrabajo.Empleado.ObtenerTodos(filtro: x=> x.StatusEmpleado==true, incluirPropiedades: "Puesto,Planta"); //El metodo obtener todos trae una lista
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
            empleadoDb.StatusEmpleado = false;
            _unidadTrabajo.Empleado.Actualizar(empleadoDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Empleado eliminado exitosamente" });
        }

        #endregion
    }

}