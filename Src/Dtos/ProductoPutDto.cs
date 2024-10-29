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
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre debe contener solo caracteres alfabéticos.")]
        [StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres.")]
        public string NombreProducto { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"Poleras|Gorros|Jugetería|Alimentación|Libros", ErrorMessage = "El tipo de producto no es válido.")]
        public string TipoProducto { get; set; } = string.Empty;

        [Required]
        [Range(1, 99999999, ErrorMessage = "El dinero debe ser un número entero positivo menor que 100 millones.")]
        public int Precio { get; set; }

        [Required]
        [Range(1, 99999, ErrorMessage = "El stock debe ser un número entero positivo menor que 100000.")]
        public int StockActual { get; set; }
    }
}