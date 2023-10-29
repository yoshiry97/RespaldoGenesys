using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe ser Máximo 60 Caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellido Paterno es requerido")]
        [MaxLength(30, ErrorMessage = "Apellido debe ser Máximo 30 Caracteres")]
        public string ApPaterno { get; set; }
        [Required(ErrorMessage = "Apellido Materno es requerido")]
        [MaxLength(30, ErrorMessage = "Apellido debe ser Máximo 30 Caracteres")]
        public string ApMaterno { get; set; }
        [Required(ErrorMessage = "Fecha de nacimiento es requerida")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Número de Seguro Social es requerido")]
        [MaxLength(11, ErrorMessage = "NSS debe ser Máximo 11 Caracteres")]
        public string NSS { get; set; }
        [Required(ErrorMessage = "CURP es requerida")]
        [MaxLength(18, ErrorMessage = "CURP debe ser Máximo 18 Caracteres")]
        public string CURP { get; set; }
        [EmailAddress(ErrorMessage = "El campo Email no tiene un formato de correo electrónico válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Puesto es requerido")]
        [Column("IdPuesto")]
        public int IdPuesto { get; set; }
        //public Puesto Puesto { get; set; } //Propiedad de navegacion para la relacion con puesto
        [Required(ErrorMessage = "Fecha de ingreso es requerida")]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "Tipo de nómina es requerida")]
        public string TipoNomina { get; set; }
        [Required(ErrorMessage = "Planta es requerida")]
        [Column("IdPlanta")]
        public int IdPlanta { get; set; }
        //public Planta Planta { get; set; } //Propiedad de navegacion para la relacion con planta
        [Required(ErrorMessage = "Turno es requerido")]
        public string Turno { get; set; }
        [Required(ErrorMessage = "Número de gafete es requerido")]
        public string NumeroGafete { get; set; }
        [Required(ErrorMessage = "Status del empleado es requerido")]
        public bool StatusEmpleado { get; set; }

    }
}
