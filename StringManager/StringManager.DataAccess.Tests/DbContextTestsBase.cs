using Microsoft.EntityFrameworkCore;

namespace StringManager.DataAccess.Tests
{
    public abstract class DbContextTestsBase
    {
        public DbContextOptions<StringManagerStorageContext> options;

        public DbContextTestsBase()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;
        }
    }
}
