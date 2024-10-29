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

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteRepository.ObtenerTodosLosClientes();
            var clienteDto = clientes.Select(c => c.ToGetClienteDto());
            return Ok(clientes);
        }

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
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] ClientePostDto postClienteDto)
        {
            var newCliente = postClienteDto.ToPostClienteDto();
            await _clienteRepository.AgregarCliente(newCliente);
            return CreatedAtAction(nameof(GetByIdCliente), new {id = newCliente.IdCliente}, newCliente.ToGetClienteDto());
        }
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