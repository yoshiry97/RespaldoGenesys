using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class DatosBancarios
    {
        [Key]
        public int IdDatosBancarios { get; set; }
        [Required(ErrorMessage = "Nombre del banco es requerido")]
        [MaxLength(50, ErrorMessage = "Nombre debe ser Máximo 50 caracteres")]
        public string NombreBanco { get; set; }
        [Required(ErrorMessage = "Número de cuenta es requerido")]
        [MaxLength(20, ErrorMessage = "Número de cuenta debe ser Máximo 20 caracteres")]
        public string NumeroCuenta { get; set; }
        [Required(ErrorMessage = "Clabe Interbancaria es requerida")]
        [MaxLength(18, ErrorMessage = "Clabe Interbancaria debe ser Máximo 18 caracteres")]
        public string ClabeInterbancaria { get; set; }
        public string Prestamos { get; set; }
        [Column("IdEmpleado")]
        public int IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public Empleado Empleado { get; set; } //Propiedad de navegacion para la clave foranea 
        public bool StatusDatosBancarios { get; set; }
    }
}
