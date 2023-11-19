using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IEmpleadoRepositorio Empleado { get; }
        IPlantaRepositorio Planta { get; }
        IPuestoRepositorio Puesto { get; }
        IDocumentosRepositorio Documentos { get; }
        IDatosBancariosRepositorio DatosBancarios { get; }
        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }
        Task Guardar();
    }
}
