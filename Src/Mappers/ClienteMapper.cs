using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos;
using api.Src.Models;

namespace api.Src.Mappers
{
    public static class ClienteMapper
    {
        /// <summary>
        /// Moldeo para cliente en metodos HttpGet
        /// </summary>
        /// <param name="clienteDisplay">el cliente a mostrar</param>
        /// <returns>un nuevo molde de clienteGet</returns>
        public static ClienteGetDto ToGetClienteDto(this Cliente clienteDisplay)
        {
            return new ClienteGetDto
            {
                IdCliente = clienteDisplay.IdCliente,
                Rut = clienteDisplay.Rut,
                NombreCliente = clienteDisplay.NombreCliente,
                FechaNacimiento = clienteDisplay.FechaNacimiento,
                Correo = clienteDisplay.Correo,
                Genero = clienteDisplay.Genero,
                Contrasenha = clienteDisplay.Contrasenha
            };
        }

        /// <summary>
        /// Moldeo para cliente en metodos HttpPost
        /// </summary>
        /// <param name="createNewClienteDto">molde para el nuevo cliente a agregar</param>
        /// <returns>un cliente a añadir</returns>
        public static Cliente ToPostClienteDto(this ClientePostDto createNewClienteDto)
        {
            return new Cliente
            {
                Rut = createNewClienteDto.Rut,
                NombreCliente = createNewClienteDto.NombreCliente,
                FechaNacimiento = createNewClienteDto.FechaNacimiento,
                Correo = createNewClienteDto.Correo,
                Genero = createNewClienteDto.Genero,
                Contrasenha = createNewClienteDto.Contrasenha
            };
        }
    }
}