using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Src.Data
{
    public class ApplicationDBContext : IdentityDbContext<UsuarioApp>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<UsuarioApp> Usuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;

        //Opcion de Seeder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {Id = "1", Name = "Administrador", NormalizedName = "ADMIN"},
                new IdentityRole {Id = "2", Name = "Usuario", NormalizedName = "USER"}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}