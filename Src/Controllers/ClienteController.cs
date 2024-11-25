using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Interfaces;
using api.Src.Mappers;
using api.Src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// Metodo Http para obtener todos los clientes
        /// </summary>
        /// <returns>ok al desplegar todos los clientes</returns>
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteRepository.ObtenerTodosLosClientes();
            var clienteDto = clientes.Select(c => c.ToGetClienteDto());
            return Ok(clientes);
        }

        /// <summary>
        /// Metodo Http para obtener un cliente por un id ingresado
        /// </summary>
        /// <param name="id">id del cliente a buscar</param>
        /// <returns>ok al encontrar el cliente, Not found de lo contrario</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCliente([FromRoute] int id)
        {
            var cliente = await _clienteRepository.ObtenerClienteById(id);
            if(cliente == null)
            {
                return NotFound("Cliente NO existente.");
            }            
            return Ok(cliente.ToGetClienteDto());
        }

        /// <summary>
        /// Metodo Http para agregar un nuevo cliente
        /// </summary>
        /// <param name="postClienteDto">modelado para agregar un nuevo cliente</param>
        /// <returns>CreateAtAction si se agrego exitosamento, bad request de lo contrario</returns>
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] ClientePostDto postClienteDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCliente = postClienteDto.ToPostClienteDto();
            await _clienteRepository.AgregarCliente(newCliente);
            return CreatedAtAction(nameof(GetByIdCliente), new {id = newCliente.IdCliente}, newCliente.ToGetClienteDto());
        }

        /// <summary>
        /// Metodo Http para modificar los datos de un producto con id coincidente a uno ingresado
        /// </summary>
        /// <param name="id">id del cliente a modificar sus datos</param>
        /// <param name="putClienteDto">modelado de datos a modificar de cliente</param>
        /// <returns>ok al modificar exitosamente, Not found si no existe el id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteIdAsync([FromRoute] int id, [FromBody] ClientePutDto putClienteDto)
        {
            var modeloClientemodificar = await _clienteRepository.ModificarCliente(id, putClienteDto);
            if(modeloClientemodificar == null)
            {
                return NotFound();
            }
            return Ok(modeloClientemodificar.ToGetClienteDto());

        }

        /// <summary>
        /// Metodo Http para eliminar un cliente con id coincidente a uno ingresado
        /// </summary>
        /// <param name="id">id del cliente a eliminar</param>
        /// <returns>No content al eliminar correctamente, Not found si no existe el id</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteId([FromRoute] int id)
        {
            var cliente = await _clienteRepository.EliminarClienteById(id);
            if(cliente == null)
            {
                return NotFound("Id cliente ingresado NO existente");
            }
            return NoContent();
        }
    }
}