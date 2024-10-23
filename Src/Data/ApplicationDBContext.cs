using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Data
{
    public class ApplicationDBContext(DbContextOptions dBContextOptions) : DbContext(dBContextOptions)
    {
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Role> Roles {get; set;} = null!;
    }
}