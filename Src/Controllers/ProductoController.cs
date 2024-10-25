using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProductoController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProductos()
        {
            var productos = _context.Productos.ToList();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdProducto([FromRoute] int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
            if(producto == null)
            {
                return NotFound("Producto NO existente.");
            }
            return Ok(producto);
        }
        [HttpPost]
        public IActionResult PostProducto([FromBody] Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return Ok(producto);
        }
        [HttpPut("{id}")]
        public IActionResult PutProductoId([FromRoute] int id, [FromBody] Producto producto)
        {
            var productoUpdate = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
            if(productoUpdate == null)
            {
                return NotFound();
            }
            productoUpdate.NombreProducto = producto.NombreProducto;
            productoUpdate.TipoProducto = producto.TipoProducto;
            productoUpdate.Precio = producto.Precio;
            productoUpdate.StockActual = producto.StockActual;

            _context.SaveChanges();
            return Ok(productoUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductoId([FromRoute] int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
            if(producto == null)
            {
                return NotFound("Id producto ingresado NO existente");
            }
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return Ok();
        }
    }
}