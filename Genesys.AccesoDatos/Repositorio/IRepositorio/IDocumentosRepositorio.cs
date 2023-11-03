using Genesys.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.AccesoDatos.Repositorio.IRepositorio
{
    public interface IDocumentosRepositorio : IRepositorio<Documentos>
    {
        void Actualizar(Documentos documentos);
        IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
    }
}
