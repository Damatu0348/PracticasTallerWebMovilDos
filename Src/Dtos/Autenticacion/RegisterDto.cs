using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Dtos.Autenticacion
{
    public class RegisterDto
    {
        [Required]
        public string? NombreCliente {get; set;} = null!;
        [Required]
        public string? Rut {get; set;} = null!;
        [Required]
        [EmailAddress]
        public string? Correo {get; set;} = null!;
        [Required]
        public string? FechaNacimiento {get; set;} = null!;
        [Required]
        public string? Genero {get; set;} = null!;
        [Required]
        [MinLength(8)]
        public string? Contrasenha {get; set;} = null!;
        
    }
}