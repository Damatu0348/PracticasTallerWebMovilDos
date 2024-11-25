using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos;
using api.Src.Models;

namespace api.Src.Mappers
{
    public static class ProductoMapper
    {
        /// <summary>
        /// Moldeo para producto en metodos HttpGet
        /// </summary>
        /// <param name="productoDisplay">el producto a mostrar</param>
        /// <returns>un nuevo molde de productoGet</returns>
        public static ProductoGetDto ToGetProductoDto(this Producto productoDisplay)
        {
            return new ProductoGetDto
            {
                IdProducto = productoDisplay.IdProducto,
                NombreProducto = productoDisplay.NombreProducto,
                Precio = productoDisplay.Precio,
                StockActual = productoDisplay.StockActual
            };
        }

        /// <summary>
        /// Moldeo para producto en metodos HttpPost
        /// </summary>
        /// <param name="createNewProductoDto">molde para el nuevo producto a agregar</param>
        /// <returns>un producto a añadir</returns>
        public static Producto ToPostProducto(this ProductoPostDto createNewProductoDto)
        {
            return new Producto
            {
                NombreProducto = createNewProductoDto.NombreProducto,
                TipoProducto = createNewProductoDto.TipoProducto,
                Precio = createNewProductoDto.Precio,
                StockActual = createNewProductoDto.StockActual
            };
        }
    }
}