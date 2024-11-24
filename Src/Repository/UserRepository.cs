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
        public async Task<UsuarioApp> AgregarUsuario(UsuarioApp usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

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

        public async Task<List<UsuarioApp>> ObtenerTodosLosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioApp?> ObtenerUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}