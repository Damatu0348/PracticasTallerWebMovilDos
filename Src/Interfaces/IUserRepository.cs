using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos.UsuariosDto;
using api.Src.Models;

namespace api.Src.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsuarioApp>> ObtenerTodosLosUsuarios();
        Task<UsuarioApp?> ObtenerUsuarioById(int id);
        Task<UsuarioApp> AgregarUsuario(UsuarioApp usuario);
        Task<UsuarioApp?> ModificarUsuario(int id, UsuarioPutDto usuarioDto);
        Task<UsuarioApp?> EliminarUsuarioById(int idEliminar);
    }
}