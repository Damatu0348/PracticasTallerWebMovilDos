using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Helpers;
using api.Src.Interfaces;
using api.Src.Models;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDBContext _context;

        /// <summary>
        /// Constructor de ProductoRepository
        /// </summary>
        /// <param name="context"></param>
        public ProductoRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo para agregar un nuevo producto
        /// </summary>
        /// <param name="producto">producto nuevo para agregar</param>
        /// <param name="imageUploadResult">conjunto de metodos para verificar subida de imagenees</param>
        /// <returns>el nuevo producto</returns>
        public async Task<Producto> AgregarProducto(Producto producto, ImageUploadResult imageUploadResult)
        {
            var product = new Producto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                TipoProducto = producto.TipoProducto,
                StockActual = producto.StockActual,
                Precio = producto.Precio,
                ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri
            };
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        /// <summary>
        /// Metodo para eliminar por id un producto existente
        /// </summary>
        /// <param name="idEliminar">id del producto a eliminar</param>
        /// <returns>el producto a eliminar</returns>
        /// <exception cref="Exception">el id no existe</exception>
        public async Task<Producto?> EliminarProductoById(int idEliminar)
        {
            var modeloProducto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == idEliminar);
            if(modeloProducto == null)
            {
                throw new Exception("Producto NO existente.");
            }
            _context.Productos.Remove(modeloProducto);
            await _context.SaveChangesAsync();
            return modeloProducto;
        }

        /// <summary>
        /// Metodo para modificar un producto con id igual a uno ingresado
        /// </summary>
        /// <param name="id">id de producto a ingresar</param>
        /// <param name="productoDto">modelado de producto para modificar</param>
        /// <returns>producto modificado</returns>
        /// <exception cref="Exception">si no existe el id</exception>
        public async Task<Producto?> ModificarProducto(int id, ProductoPutDto productoDto)
        {
            var modeloProducto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == id);
            if(modeloProducto == null)
            {
                throw new Exception("Producto NO encontrado.");
            }
            modeloProducto.NombreProducto = productoDto.NombreProducto;
            modeloProducto.TipoProducto = productoDto.TipoProducto;
            modeloProducto.Precio = productoDto.Precio;
            modeloProducto.StockActual = productoDto.StockActual;
            await _context.SaveChangesAsync();
            return modeloProducto;
        }

        /// <summary>
        /// Metodo para obtener un producto por su id
        /// </summary>
        /// <param name="id">id del producto a obtener</param>
        /// <returns>el producto con id coincidente</returns>
        public async Task<Producto?> ObtenerProductoById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        /// <summary>
        /// Metodo para obtener todos los productos
        /// 
        /// </summary>
        /// <param name="queryProducto">consulta para ayudar en la busqueda</param>
        /// <returns>lista de todos los productos</returns>
        public async Task<List<Producto>> ObtenerTodosLosProductos(QueryProducto queryProducto)
        {
            var productos = _context.Productos.AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryProducto.TipoProducto))
            {
                productos = productos.Where(p => p.TipoProducto.Contains(queryProducto.TipoProducto));
            }
            if(!string.IsNullOrWhiteSpace(queryProducto.SortBy))
            {
                if(queryProducto.SortBy.Equals("Precio", StringComparison.OrdinalIgnoreCase))
                {
                    productos = queryProducto.IsDescendiente ? productos.OrderByDescending(p => p.Precio) : productos.OrderBy(p => p.Precio);
                }
            }

            return await productos.ToListAsync();
        }
    }
}