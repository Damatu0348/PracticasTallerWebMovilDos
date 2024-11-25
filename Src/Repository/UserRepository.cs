using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos.UsuariosDto;
using api.Src.Interfaces;
using api.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Metodo para agregar un nuevo usuario en el sistema
        /// </summary>
        /// <param name="usuario">el nuevo usuario a ingresar</param>
        /// <returns>el nuevo usuario ingresado</returns>
        public async Task<UsuarioApp> AgregarUsuario(UsuarioApp usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Metodo para eliminar a un usuario por el id
        /// </summary>
        /// <param name="idEliminar">id del usuario a eliminar</param>
        /// <returns>la lista de usuarios actualizada</returns>
        /// <exception cref="Exception">si no existe el id del usuario a eliminar</exception>
        public async Task<UsuarioApp?> EliminarUsuarioById(int idEliminar)
        {
            var modeloUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdCliente == idEliminar);
            if(modeloUsuario == null)
            {
                throw new Exception("Usuario NO existente.");
            }
            _context.Usuarios.Remove(modeloUsuario);
            await _context.SaveChangesAsync();
            return modeloUsuario;
        }

        /// <summary>
        /// Metodo para modificar los datos del usuario del id elegido
        /// </summary>
        /// <param name="id">id del usuario a modificar sus datos</param>
        /// <param name="usuarioDto">modelo del usuario con datos a modificar</param>
        /// <returns>el usuario modificado</returns>
        /// <exception cref="Exception">si el id no existe, lanzamos exepcion</exception>
        public async Task<UsuarioApp?> ModificarUsuario(int id, UsuarioPutDto usuarioDto)
        {
            var modeloUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdCliente == id);
            if(modeloUsuario == null)
            {
                throw new Exception("Producto NO encontrado.");
            }
            modeloUsuario.Rut = usuarioDto.Rut;
            modeloUsuario.NombreCliente = usuarioDto.NombreCliente;
            modeloUsuario.FechaNacimiento = usuarioDto.FechaNacimiento;
            modeloUsuario.Correo = usuarioDto.Correo;
            modeloUsuario.Genero = usuarioDto.Genero;
            modeloUsuario.Contrasenha = usuarioDto.Contrasenha;
            await _context.SaveChangesAsync();
            return modeloUsuario;
        }

        /// <summary>
        /// Metodo para obtener todos los usuarios en el sistema
        /// </summary>
        /// <returns>todos los usuarios con sus datos</returns>
        public async Task<List<UsuarioApp>> ObtenerTodosLosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Metodo para obtener un usuario en el sistema por medio de un id
        /// </summary>
        /// <param name="id">id del usuario a obtener</param>
        /// <returns>el usuario del id correspondiente con todos sus datos</returns>
        public async Task<UsuarioApp?> ObtenerUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}