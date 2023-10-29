using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class Documentos
    {
        [Key]
        public int IdDocumento { get; set; }
        [Required(ErrorMessage = "Nombre de Documento es requerido")]
        [MaxLength(40, ErrorMessage = "Nombre debe ser Máximo 40 caracteres")]
        public string NombreDocumento { get; set; }
        [Required(ErrorMessage = "Archivo es requerido")]
        public byte[] Archivo { get; set; }
        [Column("IdEmpleado")]
        public int IdEmpleado { get; set; }
        //public Empleado Empleado { get; set; } //Propiedad de navegacion para la clave foranea 
        public bool StatusDocumento { get; set; }

    }
}
