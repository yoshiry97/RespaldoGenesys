using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos.ViewModels
{
    public class EmpleadoVM
    {
        public Empleado Empleado { get; set; }
        public IEnumerable<SelectListItem> PuestoLista { get; set; }
        public IEnumerable<SelectListItem> PlantaLista { get; set; }

    }
}
