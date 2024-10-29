using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Dtos;
using api.Src.Models;

namespace api.Src.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodosLosClientes();
        Task<Cliente?> ObtenerClienteById(int id);
        Task<Cliente> AgregarCliente(Cliente cliente);
        Task<Cliente?> ModificarCliente(int id, ClientePutDto clienteDto);
        Task<Cliente?> EliminarClienteById(int idEliminar);
    }
}