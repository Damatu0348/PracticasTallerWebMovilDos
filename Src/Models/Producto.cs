using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre debe contener solo caracteres alfabéticos.")]
        [StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres.")]
        public string NombreProducto { get; set; } = string.Empty;

        [RegularExpression("^(Poleras|Gorros|Jugueteria|Alimentacion|Libros)$", 
        ErrorMessage = "El tipo de producto no es válido. Los valores permitidos son: Poleras, Gorros, Jugueteria, Alimentacion, Libros.")]
        public string TipoProducto { get; set; } = string.Empty;

        [Range(1, 99999999, ErrorMessage = "El dinero debe ser un número entero positivo menor que 100 millones.")]
        public int Precio { get; set; }

        [Range(1, 99999, ErrorMessage = "El stock debe ser un número entero positivo menor que 100000.")]
        public int StockActual { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public List<Cliente> Clientes {get; set;} = [];
    }
}