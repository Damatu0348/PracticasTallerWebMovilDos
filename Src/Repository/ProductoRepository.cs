using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Interfaces;
using api.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductoRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Producto> AgregarProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

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

        public async Task<Producto?> ObtenerProductoById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<List<Producto>> ObtenerTodosLosProductos()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}