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
    public class UsuarioController : ControllerBase
    {
        private readonly IUserRepository _usuarioRepository;
        public UsuarioController(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosLosUsuarios();
            var usuarioDto = usuarios.Select(u => u.ToGetUsuarioDto());
            return Ok(usuarios);
        }

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