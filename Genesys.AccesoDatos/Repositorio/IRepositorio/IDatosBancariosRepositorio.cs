using Genesys.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio.IRepositorio
{
    public interface IDatosBancariosRepositorio : IRepositorio<DatosBancarios>
    {
        void Actualizar(DatosBancarios datosBancarios);
        IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
    }
}
