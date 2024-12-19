using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos.UsuariosDto;
using api.Src.Interfaces;
using api.Src.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserRepository _usuarioRepository;
        /// <summary>
        /// Constructor del usuario controller
        /// </summary>
        /// <param name="usuarioRepository">el repositorio de usuarios a usar</param>
        public UsuarioController(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosLosUsuarios();
            var usuarioDto = usuarios.Select(u => u.ToGetUsuarioDto());
            return Ok(usuarios);
        }

        /// <summary>
        /// Metodo HttpGet de un usario por su id
        /// </summary>
        /// <param name="id">id del usuario a obtener</param>
        /// <returns>ok si el usuario se encontro, una excepcion de lo contrario</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUsuario([FromRoute] int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioById(id);
            if(usuario == null)
            {
                return NotFound("Usuario NO existente.");
            }            
            return Ok(usuario.ToGetUsuarioDto());
        }

        /// <summary>
        /// Metodo HttpPost para modificar los datos de un usuario existente
        /// </summary>
        /// <param name="postUsuarioDto">modelo de modificacion de usuario</param>
        /// <returns>nuevo usuario creado exitosamente, bad request de lo contrario</returns>
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioPostDto postUsuarioDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nuevoUsuario = postUsuarioDto.ToPostUsuarioDto();
            await _usuarioRepository.AgregarUsuario(nuevoUsuario);
            return CreatedAtAction(nameof(GetByIdUsuario), new {id = nuevoUsuario.IdCliente}, nuevoUsuario.ToGetUsuarioDto());
        }

        /// <summary>
        /// Metodo HttpPut para modificar los datos de un usuario con un id ingresado
        /// </summary>
        /// <param name="id">id de usuario a modiifcar</param>
        /// <param name="putUsuarioDto"></param>
        /// <returns>Ok si se modifico exitosamente, Bad request de lo contrario (id no encontrado)</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioIdAsync([FromRoute] int id, [FromBody] UsuarioPutDto putUsuarioDto)
        {
            var modeloUsuarioModificar = await _usuarioRepository.ModificarUsuario(id, putUsuarioDto);
            if(modeloUsuarioModificar == null)
            {
                return NotFound();
            }
            return Ok(modeloUsuarioModificar.ToGetUsuarioDto());

        }

        /// <summary>
        /// Metodo HttpDelete para eliminar un usuario con un id ingresado
        /// </summary>
        /// <param name="id">id del usuario a eliminar</param>
        /// <returns>No content al ser exitoso, not found de lo contrario (id no existente)</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteId([FromRoute] int id)
        {
            var usuario = await _usuarioRepository.EliminarUsuarioById(id);
            if(usuario == null)
            {
                return NotFound("Id usuario a eliminar NO existente");
            }
            return NoContent();
        }
    }
}