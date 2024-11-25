using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos.UsuariosDto;
using api.Src.Models;

namespace api.Src.Mappers
{
    public static class UsuarioMapper
    {
        /// <summary>
        /// Moldeo para usuario en metodos HttpGet
        /// </summary>
        /// <param name="usuarioDisplay">el usuario a mostrar</param>
        /// <returns>un nuevo molde de usuarioGet</returns>
        public static UsuarioGetDto ToGetUsuarioDto(this UsuarioApp usuarioDisplay)
        {
            return new UsuarioGetDto
            {
                IdCliente = usuarioDisplay.IdCliente,
                Rut = usuarioDisplay.Rut,
                NombreCliente = usuarioDisplay.NombreCliente,
                FechaNacimiento = usuarioDisplay.FechaNacimiento,
                Correo = usuarioDisplay.Correo,
                Genero = usuarioDisplay.Genero,
                Contrasenha = usuarioDisplay.Contrasenha
            };
        }

        /// <summary>
        /// Moldeo para usuario en metodos HttpPost
        /// </summary>
        /// <param name="createNewUsuarioDto">molde para el nuevo usuario a agregar</param>
        /// <returns>un usuario a añadir</returns>
        public static UsuarioApp ToPostUsuarioDto(this UsuarioPostDto createNewUsuarioDto)
        {
            return new UsuarioApp
            {
                Rut = createNewUsuarioDto.Rut,
                NombreCliente = createNewUsuarioDto.NombreCliente,
                FechaNacimiento = createNewUsuarioDto.FechaNacimiento,
                Correo = createNewUsuarioDto.Correo,
                Genero = createNewUsuarioDto.Genero,
                Contrasenha = createNewUsuarioDto.Contrasenha
            };
        }
    }
}