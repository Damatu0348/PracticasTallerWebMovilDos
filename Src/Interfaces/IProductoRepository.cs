using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos;
using api.Src.Helpers;
using api.Src.Models;

namespace api.Src.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosLosProductos(QueryProducto queryProducto);
        Task<Producto?> ObtenerProductoById(int id);
        Task<Producto> AgregarProducto(Producto producto);
        Task<Producto?> ModificarProducto(int id, ProductoPutDto productoDto);
        Task<Producto?> EliminarProductoById(int idEliminar);

    }
}