using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;

namespace api.Src.Dtos.Autenticacion
{
    public class NuevoUsuarioDto
    {
        public string NombreCliente { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Token {get; set;} = null!;
    }
}