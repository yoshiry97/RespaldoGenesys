using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class Puesto
    {
        [Key]
        public int IdPuesto { get; set; }
        [Required(ErrorMessage = "Nombre del puesto es requerido")]
        [MaxLength(30, ErrorMessage = "Nombre debe ser Máximo 30 Caracteres")]
        public string NombrePuesto { get; set; }
        [Required(ErrorMessage = "Descripción es requerida")]
        [MaxLength(100, ErrorMessage = "Descripción debe ser Máximo 100 Caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Sueldo del puesto es requerido")]
        public decimal Sueldo { get; set; }
        public bool StatusPuesto { get; set; }
    }
}
