using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess;
using System.Linq;

namespace StringManager.Tests.IntegrationTests.Setup
{
    public class TestWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        private ILogger<TestWebApplicationFactory<TStartup>> logger;
        private OnMemoryDatabaseHandler databaseHandler;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<StringManagerStorageContext>));

                services.Remove(descriptor);

                services.AddDbContext<StringManagerStorageContext>(options =>
                {
                    options.UseInMemoryDatabase("TestStringManagerDatabase");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    
                    var scopedServices = scope.ServiceProvider;
                    databaseHandler = new OnMemoryDatabaseHandler(scopedServices.GetRequiredService<StringManagerStorageContext>());
                    logger = scopedServices
                        .GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();

                    SetupDatabase();
                }
            });
        }

        private void SetupDatabase()
        {
            try
            {
                databaseHandler.InitializeDbForTests();
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding database with test data; error: {Message}", ex.Message);
            }
        }

        public void DropDatabase()
        {
            try
            {
                databaseHandler.DropTestDb();
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "An error occurred during deletion of a database with test messages; error: {Message}", ex.Message);
            }
        }
    }
}
