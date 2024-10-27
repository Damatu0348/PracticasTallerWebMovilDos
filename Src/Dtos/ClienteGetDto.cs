using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;

namespace api.Src.Dtos
{
    public class ClienteGetDto
    {
        [Required]
        public int IdCliente { get; set; }
        
        [Required]
        public string Rut { get; set; } = string.Empty;
        
        [Required]
        public string NombreCliente { get; set; } = string.Empty;

        [Required]
        public string FechaNacimiento { get; set; } = string.Empty;

        [Required]
        public string Correo { get; set; } = string.Empty;
        
        [Required]
        public string Genero { get; set; } = string.Empty;
        
        [Required]
        public string Contrasenha { get; set; } = string.Empty;
    }
}