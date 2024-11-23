using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;
using Bogus;

namespace api.Src.Data
{
    public class DataSeeder
    {
        private static Random random = new Random();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDBContext>();
                var tiposProducto = new string[] {"Poleras", "Gorros", "Jugeteria", "Alimentación", "Libros"};

                if(!context.Productos.Any())
                {
                    var productFaker = new Faker<Producto>()
                        .RuleFor(p => p.NombreProducto, f => f.Commerce.ProductName())
                        .RuleFor(p => p.TipoProducto, f => tiposProducto[random.Next(0, tiposProducto.Length)])
                        .RuleFor(p => p.Precio, f => f.Random.Number(1, 99999999))
                        .RuleFor(p => p.StockActual, f => f.Random.Number(1, 99999))
                        .RuleFor(p => p.ImageUrl, f => "https://png.pngtree.com/png-vector/20190809/ourmid/pngtree-packaging-branding-marketing-product-bottle-flat-color-icon-png-image_1652448.jpg");

                    var products = productFaker.Generate(10);
                    context.Productos.AddRange(products);
                    context.SaveChanges();
                }
                /*
                var admin = new UsuarioApp
                {
                    ...
                }
                */
                //context.Users.Add(admin);
                
                context.SaveChanges();
            }
        }
    }
}