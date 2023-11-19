using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesys.Modelos
{
    public class UsuarioAplicacion : IdentityUser

    {
        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(80)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellido es Requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }
        [NotMapped]
        public string Role { get; set; }

    }
}
