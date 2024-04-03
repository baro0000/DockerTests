using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOTags.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTags.Tests.UnitTests.SOTags.DataAccessTests
{
    public class ConnectionWithDatabaseTest
    {
        private readonly DatabaseDbContext context;

        public ConnectionWithDatabaseTest()
        {
            // Konfiguracja usług dla testów
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Twoja konfiguracja połączenia z bazą danych
                .Build();

            var services = new ServiceCollection()
                .AddDbContext<DatabaseDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DatabaseDbContext")))
                .BuildServiceProvider();

            context = services.GetRequiredService<DatabaseDbContext>();
        }

        [Fact]
        public async Task CanConnectToDatabase()
        {
            try
            {
                // Act
                var connection = await context.Database.CanConnectAsync();

                // Assert
                Assert.True(connection);
            }
            catch (Exception ex)
            {
                // Jeśli nawiązanie połączenia nie powiodło się, wypisz błąd
                Console.WriteLine($"Connection failed: {ex.Message}");
                Assert.True(false); // Nie powiodło się nawiązanie połączenia
            }
        }
    }
}
