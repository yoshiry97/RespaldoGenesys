using Genesys.AccesoDatos.Repositorio.IRepositorio;
using Genesys.Modelos;
using Genesys.Utilidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Genesys.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Upsert(int? id) //id lleva signo de pregunta porque puede ir vacio
        {
            Documentos documentos = new Documentos();
            if (id == null)
            {
                //Crear una nuevo empleado
                documentos.StatusDocumento = true;
                return View(documentos);
            }
            //Actualizamos empleado
            documentos = await _unidadTrabajo.Documentos.Obtener(id.GetValueOrDefault());
            if (documentos == null)
            {
                return NotFound();
            }
            return View(documentos);
        }
        //Vamos a crear el Upsert Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert([FromForm] Documentos documentos, IFormFile archivo)
        {
            if (ModelState.IsValid)
            {
                if (archivo != null && archivo.Length > 0)
                {
                    // Guardar el archivo en una carpeta en el sistema de archivos
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + archivo.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }

                    // Asigna los datos binarios del archivo a la propiedad "Archivo"
                    using (var memoryStream = new MemoryStream())
                    {
                        await archivo.CopyToAsync(memoryStream);
                        documentos.Archivo = memoryStream.ToArray();
                    }

                    documentos.NombreDocumento = uniqueFileName; // Guarda el nombre del archivo

                    if (documentos.IdDocumento == 0)
                    {
                        await _unidadTrabajo.Documentos.Agregar(documentos);
                        TempData[DS.Exitosa] = "Documento Creado Exitosamente";
                    }
                    else
                    {
                        _unidadTrabajo.Documentos.Actualizar(documentos);
                        TempData[DS.Exitosa] = "Documento Actualizado Exitosamente";
                    }

                    await _unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                TempData[DS.Error] = "Error al Grabar Documento";
            }

            return View(documentos);
        }
        #region API
        [HttpGet]
        //El codigo de la region sirve mas que nada para poner comentarios
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Documentos.ObtenerTodos(); //El metodo obtener todos trae una lista
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
        [ActionName("DescargarArchivo")]
        public async Task<IActionResult> DescargarArchivo(int id)
        {
            var documento = await _unidadTrabajo.Documentos.Obtener(id); // Asegúrate de usar await aquí
            if (documento == null)
            {
                // Manejar el caso en el que el documento no se encuentre
                return NotFound();
            }

            // Accede a las propiedades del documento
            byte[] archivoData = documento.Archivo;
            string nombreArchivo = documento.NombreDocumento;

            // Devuelve el archivo como una descarga
            return File(archivoData, "application/octet-stream", nombreArchivo);
        }

        #endregion
    }
}
