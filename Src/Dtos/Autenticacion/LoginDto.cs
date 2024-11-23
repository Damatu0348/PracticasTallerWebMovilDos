using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Dtos.Autenticacion
{
    public class LoginDto
    {
        [Required]
        public string Correo {get; set;} = null!;
        [Required]
        public string Contrasenha {get; set;} = null!;
    }
}