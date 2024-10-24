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
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { NombreRole = "Administrador" },
                        new Role { NombreRole = "Cliente" }
                    );
                    context.SaveChanges();
                }
                var tiposProducto = new string[] {"Poleras", "Gorros", "Jugeteria", "Alimentación", "Libros"};
                if(!context.Productos.Any())
                {
                    var productFaker = new Faker<Producto>()
                        .RuleFor(p => p.NombreProducto, f => f.Commerce.ProductName())
                        .RuleFor(p => p.TipoProducto, f => tiposProducto[random.Next(0, tiposProducto.Length)])
                        .RuleFor(p => p.Precio, f => f.Random.Number(1, 99999999))
                        .RuleFor(p => p.StockActual, f => f.Random.Number(1, 99999));

                    var products = productFaker.Generate(10);
                    context.Productos.AddRange(products);
                    context.SaveChanges();
                }
                var generos = new string[] {"Masculino", "Femenino", "Otro", "Prefiero no decirlo"};
                var existingRuts = new HashSet<string>();
                if(!context.Clientes.Any())
                {
                    var userFaker = new Faker<Cliente>()
                        .RuleFor(c => c.Rut, f => GenerateUniqueRandomRut(existingRuts))
                        .RuleFor(c => c.NombreCliente, f => f.Person.FullName)
                        .RuleFor(c => c.FechaNacimiento, f => GenerarFechaNacimientoAleatoria())
                        .RuleFor(c => c.Correo, f => f.Person.Email)
                        .RuleFor(c => c.Genero, f => generos[random.Next(0, generos.Length)])
                        .RuleFor(c => c.Contrasenha, f => GenerarContraseñaAleatoria())
                        .RuleFor(c => c.RoleId, f => f.Random.Number(1, 2));

                    var users = userFaker.Generate(10);
                    context.Clientes.AddRange(users);
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
            if(verificator == 10){
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
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int longitudContraseña = random.Next(8, 21); // Generar una longitud entre 8 y 20 caracteres

            return new string(Enumerable.Repeat(caracteres, longitudContraseña)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}