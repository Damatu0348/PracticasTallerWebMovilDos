using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Src.Models
{
    public class UsuarioApp : IdentityUser
    {
        
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "El RUT debe tener entre 9 y 12 caracteres.")]
        public string? Rut {get; set;} = null!;
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Cliente), "ValidateBirthDate")]
        public string? FechaNacimiento {get; set;} = null!;
        [Required]
        [RegularExpression(@"Masculino|Femenino|Otro|Prefiero no decirlo", ErrorMessage = "El género no es válido.")]
        public string? Genero {get; set;} = null!;     

        public static ValidationResult? ValidateBirthDate(DateTime FechaNacimiento, ValidationContext context)
        {
            return FechaNacimiento < DateTime.Now ? ValidationResult.Success : new ValidationResult("La fecha de nacimiento debe ser menor a la fecha actual.");
        }   
    }
}