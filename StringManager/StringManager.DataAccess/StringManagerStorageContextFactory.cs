using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StringManager.DataAccess
{
    public class StringManagerStorageContextFactory : IDesignTimeDbContextFactory<StringManagerStorageContext>
    {
        public StringManagerStorageContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile($"{currentDirectory}/../StringManager/appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<StringManagerStorageContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("StringManagerConnectionString"));
            return new StringManagerStorageContext(optionsBuilder.Options);
        }
    }
}
