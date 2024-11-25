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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ClienteRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<Cliente> AgregarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEliminar"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Metodo para obtener un client epor medio de un id
        /// </summary>
        /// <param name="id">id del lciente a obtener</param>
        /// <returns>el cliente con el id a buscar</returns>
        public async Task<Cliente?> ObtenerClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        /// <summary>
        /// Metodo para obtener todos los clientes
        /// </summary>
        /// <returns>lista de todos los clientes</returns>
        public async Task<List<Cliente>> ObtenerTodosLosClientes()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}