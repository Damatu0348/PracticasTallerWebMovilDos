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
        [Key]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "El RUT debe tener entre 9 y 12 caracteres.")]
        public string Rut {get; set;} = null!;
        [StringLength(225, MinimumLength = 8)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Cliente), "ValidateBirthDate")]
        public string BirthDate {get; set;} = null!;
        [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
        public new string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"Masculino|Femenino|Otro|Prefiero no decirlo", ErrorMessage = "El género no es válido.")]
        public string Genero {get; set;} = null!;    
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{8,20}$", ErrorMessage = "La contraseña debe ser alfanumérica y tener entre 8 y 20 caracteres.")]
        public string Password { get; set; } = string.Empty; 

        public static ValidationResult? ValidateBirthDate(DateTime FechaNacimiento, ValidationContext context)
        {
            return FechaNacimiento < DateTime.Now ? ValidationResult.Success : new ValidationResult("La fecha de nacimiento debe ser menor a la fecha actual.");
        }   
    }
}