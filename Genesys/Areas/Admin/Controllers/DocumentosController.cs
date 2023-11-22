using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Modelos.ViewModels;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Auxiliar)]
    public class DocumentosController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DocumentosController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            DocumentosVM documentosVM = new DocumentosVM()
            {
                Documentos = new Documentos(),
                EmpleadoLista = _unidadTrabajo.Documentos.ObtenerTodosDropdownLista("Empleado")
            };

            if (id == null)
            {
                // Crear nuevo Documento
                documentosVM.Documentos.StatusDocumento = true;
                return View(documentosVM);
            }
            else
            {
                documentosVM.Documentos = await _unidadTrabajo.Documentos.Obtener(id.GetValueOrDefault());
                if (documentosVM.Documentos == null)
                {
                    return NotFound();
                }
                return View(documentosVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DocumentosVM documentosVM)
        {
            if (ModelState.IsValid)
            {
                //return Content("Url correcta!");
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (documentosVM.Documentos.IdDocumento == 0)
                {
                    // Crear
                    string upload = webRootPath + DS.ImagenDocumentosRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    documentosVM.Documentos.ArchivoUrl = fileName + extension;
                    
                    await _unidadTrabajo.Documentos.Agregar(documentosVM.Documentos);
                }
                else
                {
                    // Actualizar
                    var objDocumento = await _unidadTrabajo.Documentos.ObtenerPrimero(p => p.IdDocumento == documentosVM.Documentos.IdDocumento, isTracking: false);
                    if (files.Count > 0)  // Si se carga una nueva Imagen para el producto existente
                    {
                        string upload = webRootPath + DS.ImagenDocumentosRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objDocumento.ArchivoUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        documentosVM.Documentos.ArchivoUrl = fileName + extension;
                      
                    } // Caso contrario no se carga una nueva imagen
                    else
                    {
                        documentosVM.Documentos.ArchivoUrl = objDocumento.ArchivoUrl;
                       
                    }
                    _unidadTrabajo.Documentos.Actualizar(documentosVM.Documentos);
                }
                TempData[DS.Exitosa] = "Transaccion Exitosa!";
                await _unidadTrabajo.Guardar();
                //return View("Index");
                return RedirectToAction("Index");
            }
            documentosVM.EmpleadoLista = _unidadTrabajo.Documentos.ObtenerTodosDropdownLista("Empleado");
          
            return View(documentosVM);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Documentos.ObtenerTodos(incluirPropiedades:"Empleado"); //El metodo obtener todos trae una lista
            return Json(new { data = todos }); //todos tiene la lista de empleados, data lo referenciaremos desde el javascript
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var documentosBD = await _unidadTrabajo.Documentos.Obtener(id);
            if (documentosBD == null)
            {
                return Json(new { success = false, message = "Error al borrar documentos" });
            }
            //Remover imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenDocumentosRuta;
            var anteriorFile = Path.Combine(upload, documentosBD.ArchivoUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _unidadTrabajo.Documentos.Remover(documentosBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Documento eliminado exitosamente" });
        }
       
        
        
        
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Documentos.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.NombreDocumento.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombreDocumento.ToLower().Trim() == nombre.ToLower().Trim() && b.IdDocumento != id);
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
