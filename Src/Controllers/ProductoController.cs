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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly Cloudinary _cloudinary;
        public ProductoController(IProductoRepository productoRepository, Cloudinary cloudinary)
        {
            _productoRepository = productoRepository;
            _cloudinary = cloudinary;
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var producto = await _productoRepository.ObtenerProductoById(id);
            if(producto == null)
            {
                return NotFound("Producto NO existente.");
            }
            return Ok(producto);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostProducto([FromBody] ProductoPostDto postProductoDto)
        {
            if(postProductoDto.Image == null || postProductoDto.Image.Length == 0)
            {
                return BadRequest("Image is required");
            }

            if (postProductoDto.Image.ContentType != "image/png" && postProductoDto.Image.ContentType != "image/jpeg")
            {
                return BadRequest("Only PNG and JPG images are allowed.");
            }

            if (postProductoDto.Image.Length > 10 * 1024 * 1024)
            {
                return BadRequest("Image size must not exceed 10 MB.");
            }

             var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(postProductoDto.Image.FileName, postProductoDto.Image.OpenReadStream()),
                Folder = "product_images"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return BadRequest(uploadResult.Error.Message);
            }
            var newProducto = postProductoDto.ToPostProducto();
            await _productoRepository.AgregarProducto(newProducto, uploadResult);
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