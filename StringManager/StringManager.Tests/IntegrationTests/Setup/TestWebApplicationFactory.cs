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
        private StringManagerStorageContext dbContext;
        private ILogger<TestWebApplicationFactory<TStartup>> logger;

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
                    dbContext = scopedServices.GetRequiredService<StringManagerStorageContext>();
                    logger = scopedServices
                        .GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();

                    setupDatabase();
                }
            });
        }

        private void setupDatabase()
        {
            dbContext.Database.EnsureCreated();

            try
            {
                OnMemoryDatabaseHandling.InitializeDbForTests(dbContext);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding database with test data; error: {Message}", ex.Message);
            }
        }

        public void dropDatabase()
        {
            try
            {
                OnMemoryDatabaseHandling.DropTestDb(dbContext);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "An error occurred during deletion of a database with test messages; error: {Message}", ex.Message);
            }
        }
    }
}
