using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Helpers;
using api.Src.Interfaces;
using api.Src.Mappers;
using api.Src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos([FromQuery] QueryProducto queryProducto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productos = await _productoRepository.ObtenerTodosLosProductos(queryProducto);
            var productoDto = productos.Select(p => p.ToGetProductoDto());
            return Ok(productoDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProducto([FromRoute] int id)
        {
            var producto = await _productoRepository.ObtenerProductoById(id);
            if(producto == null)
            {
                return NotFound("Producto NO existente.");
            }
            return Ok(producto.ToGetProductoDto());
        }
        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] ProductoPostDto postProductoDto)
        {
            var newProducto = postProductoDto.ToPostProducto();
            await _productoRepository.AgregarProducto(newProducto);
            return CreatedAtAction(nameof(GetByIdProducto), new {id = newProducto.IdProducto }, newProducto.ToGetProductoDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutProductoId([FromRoute] int id, [FromBody] ProductoPutDto putProductoDto)
        {
            var modeloProductoModificar = await _productoRepository.ModificarProducto(id, putProductoDto);
            if(modeloProductoModificar == null)
            {
                return NotFound();
            }
            return Ok(modeloProductoModificar.ToGetProductoDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoId([FromRoute] int id)
        {
            var producto = await _productoRepository.EliminarProductoById(id);
            if(producto == null)
            {
                return NotFound("Id producto ingresado NO existente");
            }
            return NoContent();
        }
    }
}