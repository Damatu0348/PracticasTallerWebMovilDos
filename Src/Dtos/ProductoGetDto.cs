using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Dtos
{
    //Datos que mostrara el producto al usar metodos HttpGet
    public class ProductoGetDto
    {
        [Required]
        public int IdProducto { get; set; }
        
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