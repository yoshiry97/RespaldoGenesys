using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class Planta
    {
        [Key]
        public int IdPlanta { get; set; }
        [Required(ErrorMessage = "Nombre Planta es Requerido")]
        [MaxLength(30, ErrorMessage = "Nombre debe ser Máximo 30 Caracteres")]
        public string NombrePlanta { get; set; }
        public bool StatusPlanta { get; set; }

    }
}
