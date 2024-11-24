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