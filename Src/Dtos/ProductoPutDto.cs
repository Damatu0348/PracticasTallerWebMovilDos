using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Dtos
{
    //Los datos a modificar de un producto con metodos HttpPut
    public class ProductoPutDto
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