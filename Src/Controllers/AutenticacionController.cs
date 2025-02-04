using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos.Autenticacion;
using api.Src.Interfaces;
using api.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<UsuarioApp> _signInManager;
        /// <summary>
        /// Constructor de autenticacion COntroller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="tokenService"></param>
        /// <param name="signInManager"></param>
        public AutenticacionController(UserManager<UsuarioApp> userManager, ITokenService tokenService, SignInManager<UsuarioApp> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Metodo que registra un nuevo usuario en el sistema
        /// </summary>
        /// <param name="registerDto">el modelo de registro del usuario a ingresar</param>
        /// <returns>Ok si se registro exitosamente, error 500 de lo contrario</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var usuarioApp = new UsuarioApp
                {
                    UserName = registerDto.NombreCliente,
                    Rut = registerDto.Rut,
                    Email = registerDto.Correo,
                    BirthDate = registerDto.FechaNacimiento,
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
                        return Ok(
                            new NuevoUsuarioDto
                            {
                                NombreCliente = usuarioApp.UserName,
                                Correo = usuarioApp.Email,
                                Token = _tokenService.CreateToken(usuarioApp)
                            }                            
                        );
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
    
        /// <summary>
        /// Metodo para ingresar a sesion o Logearse como usuario existente en el sistema
        /// </summary>
        /// <param name="loginDto">modelo de inicio de sesion para usuario</param>
        /// <returns>Ok si se pudo logear con exito, error 500 de lo contrario</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var usuarioCorreo = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Correo);
                if(usuarioCorreo == null)
                {
                    return Unauthorized("Correo invalido");
                }
                var usuarioContrasenha = await _signInManager.CheckPasswordSignInAsync(usuarioCorreo, loginDto.Contrasenha, false);
                if(!usuarioContrasenha.Succeeded)
                {
                   return Unauthorized("Contraseña incorrecta");
                }
                return Ok
                (
                    new NuevoUsuarioDto
                    {
                        NombreCliente = usuarioCorreo.UserName!,
                        Correo = usuarioCorreo.Email!,
                        Token = _tokenService.CreateToken(usuarioCorreo)
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}