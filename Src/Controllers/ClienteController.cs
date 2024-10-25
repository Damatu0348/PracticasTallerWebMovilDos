using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
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
            var clientes = _context.Clientes.Include(c => c.Role).ToList();
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
            return Ok(cliente);
        }
        [HttpPost]
        public IActionResult PostCliente([FromBody] Cliente cliente)
        {
            var role = _context.Roles.FirstOrDefault(r => r.IdRole == cliente.RoleId);
            if(role == null)
            {   
                return BadRequest("Role no encontrado");
            }
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return Ok(cliente);
        }
        [HttpPut("{id}")]
        public IActionResult PutClienteId([FromRoute] int id, [FromBody] Cliente cliente)
        {
            var role = _context.Roles.FirstOrDefault(r => r.IdRole == cliente.RoleId);
            if(role == null)
            {
                return BadRequest("Role no encontrado");
            }
            var clienteUpdate = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if(clienteUpdate == null)
            {
                return NotFound();
            }
            clienteUpdate.Rut = cliente.Rut;
            clienteUpdate.NombreCliente = cliente.NombreCliente;
            clienteUpdate.FechaNacimiento = cliente.FechaNacimiento;
            clienteUpdate.Correo = cliente.Correo;
            clienteUpdate.Contrasenha = cliente.Contrasenha;

            _context.SaveChanges();
            return Ok(clienteUpdate);

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