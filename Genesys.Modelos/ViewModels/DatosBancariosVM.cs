using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos.ViewModels
{
    public class DatosBancariosVM
    {
        public DatosBancarios datosBancarios { get; set; }
        public IEnumerable<SelectListItem> EmpleadoLista { get; set; }
    }
}
