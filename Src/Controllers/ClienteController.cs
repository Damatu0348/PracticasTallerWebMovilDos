using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Mappers;
using api.Src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ClienteController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _context.Clientes.Include(c => c.Role).ToList().Select(c => c.ToGetClienteDto());
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdCliente([FromRoute] int id)
        {
            var cliente = _context.Clientes.Include(c => c.Role).FirstOrDefault(c => c.IdCliente == id);
            if(cliente == null)
            {
                return NotFound("Cliente NO existente.");
            }
            return Ok(cliente.ToGetClienteDto());
        }
        [HttpPost]
        public IActionResult PostCliente([FromBody] ClientePostDto postClienteDto)
        {
            var newCliente = postClienteDto.ToPostClienteDto();
            _context.Clientes.Add(newCliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByIdCliente), new {id = newCliente.IdCliente}, newCliente.ToGetClienteDto());
        }
        [HttpPut("{id}")]
        public IActionResult PutClienteId([FromRoute] int id, [FromBody] ClientePutDto putClienteDto)
        {
            var clienteExiste = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if(clienteExiste == null)
            {
                return NotFound();
            }
            clienteExiste.Rut = putClienteDto.Rut;
            clienteExiste.NombreCliente = putClienteDto.NombreCliente;
            clienteExiste.FechaNacimiento = putClienteDto.FechaNacimiento;
            clienteExiste.Correo = putClienteDto.Correo;
            clienteExiste.Contrasenha = putClienteDto.Contrasenha;

            _context.SaveChanges();
            return Ok(clienteExiste);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClienteId([FromRoute] int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if(cliente == null)
            {
                return NotFound("Id ingresado NO existente");
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok();
        }
    }
}