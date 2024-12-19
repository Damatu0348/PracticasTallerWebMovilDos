using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Src.Models;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;

namespace api.Src.Data
{
    public class DataSeeder
    {
        private static Random random = new Random();

        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDBContext>();
                var userManager = services.GetRequiredService<UserManager<UsuarioApp>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var tiposProducto = new string[]
                {
                    "Poleras",
                    "Gorros",
                    "Jugeteria",
                    "Alimentación",
                    "Libros",
                };
                var generos = new string[]
                {
                    "Masculino",
                    "Femenino",
                    "Otro",
                    "Prefiero no decirlo",
                };
                var existingRuts = new HashSet<string>();
                var faker = new Faker();

                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new IdentityRole
                    {
                        Id = "2",
                        Name = "Client",
                        NormalizedName = "USER",
                    },
                };

                foreach (var role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role.Name))
                    {
                        await roleManager.CreateAsync(role);
                        context.SaveChanges();
                    }
                }

                if (!context.Productos.Any())
                {
                    var productFaker = new Faker<Producto>()
                        .RuleFor(p => p.NombreProducto, f => f.Commerce.ProductName())
                        .RuleFor(
                            p => p.TipoProducto,
                            f => tiposProducto[random.Next(0, tiposProducto.Length)]
                        )
                        .RuleFor(p => p.Precio, f => f.Random.Number(1, 99999999))
                        .RuleFor(p => p.StockActual, f => f.Random.Number(1, 99999))
                        .RuleFor(
                            p => p.ImageUrl,
                            f =>
                                "https://png.pngtree.com/png-vector/20190809/ourmid/pngtree-packaging-branding-marketing-product-bottle-flat-color-icon-png-image_1652448.jpg"
                        );

                    var products = productFaker.Generate(10);
                    context.Productos.AddRange(products);
                    context.SaveChanges();
                }

                if (!userManager.Users.Any())
                {
                    var admin = new UsuarioApp
                    {
                        Rut = "20.416.699-4.",
                        Name = "Ignacio Mancilla",
                        UserName = "IgnacioMancilla",
                        BirthDate = "2000-10-25",
                        Email = "admin@idwm.cl",
                        Genero = "Masculino",
                    };

                    var resultadoAdmi = await userManager.CreateAsync(admin, "P4ssw0rd");
                    if (resultadoAdmi.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                        context.SaveChanges();
                    }
                    else
                    {
                        foreach (var error in resultadoAdmi.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        faker = new Faker();

                        var usuarioAppSedder = new UsuarioApp
                        {
                            Name = faker.Person.FullName,
                            UserName = faker.Person.UserName,
                            Rut = GenerateUniqueRandomRut(existingRuts),
                            BirthDate = GenerarFechaNacimientoAleatoria(),
                            Email = faker.Person.Email,
                            Genero = generos[random.Next(0, generos.Length)],
                            Password = GenerarContraseñaAleatoria(),
                        };

                        var createUser = await userManager.CreateAsync(usuarioAppSedder);
                        if (!createUser.Succeeded)
                        {
                            throw new Exception("Error al crear el usuario");
                        }

                        var roleResult = userManager.AddToRoleAsync(usuarioAppSedder, "Client");

                        if (roleResult.Result.Succeeded)
                        {
                            Console.WriteLine(
                                $"Usuario {usuarioAppSedder.Name} creado exitosamente"
                            );
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Error al asignar rol al usuario");
                        }
                    }
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
        }

        private static string GenerarFechaNacimientoAleatoria()
        {
            var random = new Random();
            int startYear = DateTime.Now.Year - 100; // Fecha mínima hace 100 años
            int endYear = DateTime.Now.Year - 18; // Fecha máxima hace 18 años (mayor de edad)
            int year = random.Next(startYear, endYear);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            var fechaNacimiento = new DateTime(year, month, day);
            return fechaNacimiento.ToString("yyyy-MM-dd");
        }

        private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
        {
            string rut;
            do
            {
                rut = GenerateRandomRut();
            } while (existingRuts.Contains(rut));
            existingRuts.Add(rut);
            return rut;
        }

        private static string GenerateRandomRut()
        {
            Random random = new();
            int rutNumber = random.Next(1, 99999999); // Número aleatorio de 7 o 8 dígitos
            int verificator = CalculateRutVerification(rutNumber);
            string verificatorStr = verificator.ToString();
            if (verificator == 10)
            {
                verificatorStr = "k";
            }
            return $"{rutNumber}-{verificatorStr}";
        }

        private static int CalculateRutVerification(int rutNumber)
        {
            int[] coefficients = { 2, 3, 4, 5, 6, 7 };
            int sum = 0;
            int index = 0;
            while (rutNumber != 0)
            {
                sum += rutNumber % 10 * coefficients[index];
                rutNumber /= 10;
                index = (index + 1) % 6;
            }
            int verification = 11 - (sum % 11);
            return verification == 11 ? 0 : verification;
        }

        private static string GenerarContraseñaAleatoria()
        {
            var random = new Random();
            const string caracteres =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int longitudContraseña = random.Next(8, 21); // Generar una longitud entre 8 y 20 caracteres
            return new string(
                Enumerable
                    .Repeat(caracteres, longitudContraseña)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()
            );
        }
    }
}
