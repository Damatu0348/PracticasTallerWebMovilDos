using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Dtos
{
    //Los datos a agregar del nuevo producto al hacer metodos HttpPost
    public class ProductoPostDto
    {
        [Required]
        public string NombreProducto { get; set; } = string.Empty;

        [Required]
        public string TipoProducto { get; set; } = string.Empty;

        [Required]
        public int Precio { get; set; }

        [Required]
        public int StockActual { get; set; }
        
    }
}