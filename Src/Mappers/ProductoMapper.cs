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