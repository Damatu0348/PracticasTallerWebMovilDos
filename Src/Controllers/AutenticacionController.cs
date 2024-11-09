using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos.Autenticacion;
using api.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly UserManager<UsuarioApp> _userManager;
        public AutenticacionController(UserManager<UsuarioApp> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                
                var usuarioApp = new UsuarioApp
                {
                    UserName = registerDto.NombreCliente,
                    Rut = registerDto.Rut,
                    Email = registerDto.Correo,
                    FechaNacimiento = registerDto.FechaNacimiento,
                    Genero = registerDto.Genero,            
                };
                if(string.IsNullOrEmpty(registerDto.Contrasenha))
                {
                    return BadRequest("La contraseña no DEBE estar vacia.");
                }
                var usuarioCrear = await _userManager.CreateAsync(usuarioApp, registerDto.Contrasenha);
                if(usuarioCrear.Succeeded)
                {
                    var roleCrear = await _userManager.AddToRoleAsync(usuarioApp, "User");
                    if(roleCrear.Succeeded)
                    {
                        return Ok("¡Usuario creado exitosamente!");
                    }
                    else
                    {
                        return StatusCode(500, roleCrear.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, usuarioCrear.Errors);
                }                                  
            }
            catch(Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
    }
}