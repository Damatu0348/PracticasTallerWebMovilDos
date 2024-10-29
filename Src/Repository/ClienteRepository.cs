using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Data;
using api.Src.Dtos;
using api.Src.Interfaces;
using api.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDBContext _context;
        public ClienteRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AgregarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> EliminarClienteById(int idEliminar)
        {
            var modeloCliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == idEliminar);
            if(modeloCliente == null)
            {
                throw new Exception("Cliente NO existente.");
            }
            _context.Clientes.Remove(modeloCliente);
            await _context.SaveChangesAsync();
            return modeloCliente;
        }

        public async Task<Cliente?> ModificarCliente(int id, ClientePutDto clienteDto)
        {
            var modeloCliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if(modeloCliente == null)
            {
                throw new Exception("Producto NO encontrado.");
            }
            modeloCliente.Rut = clienteDto.Rut;
            modeloCliente.NombreCliente = clienteDto.NombreCliente;
            modeloCliente.FechaNacimiento = clienteDto.FechaNacimiento;
            modeloCliente.Correo = clienteDto.Correo;
            modeloCliente.Genero = clienteDto.Genero;
            modeloCliente.Contrasenha = clienteDto.Contrasenha;
            await _context.SaveChangesAsync();
            return modeloCliente;
        }

        public async Task<Cliente?> ObtenerClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Cliente>> ObtenerTodosLosClientes()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}