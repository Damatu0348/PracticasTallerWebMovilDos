using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Mappers;
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
            var productos = _context.Productos.ToList().Select(p => p.ToGetProductoDto());
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
            return Ok(producto.ToGetProductoDto());
        }
        [HttpPost]
        public IActionResult PostProducto([FromBody] ProductoPostDto postProductoDto)
        {
            var newProducto = postProductoDto.ToPostProducto();
            _context.Productos.Add(newProducto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByIdProducto), new {id = newProducto.IdProducto }, newProducto.ToGetProductoDto());
        }
        [HttpPut("{id}")]
        public IActionResult PutProductoId([FromRoute] int id, [FromBody] ProductoPutDto putProductoDto)
        {
            var productoExist = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
            if(productoExist == null)
            {
                return NotFound();
            }
            productoExist.NombreProducto = putProductoDto.NombreProducto;
            productoExist.TipoProducto = putProductoDto.TipoProducto;
            productoExist.Precio = putProductoDto.Precio;
            productoExist.StockActual = putProductoDto.StockActual;

            _context.SaveChanges();
            return Ok(productoExist);
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