
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StringManager.DataAccess
{
    public class StringManagerStorageContextFactory : IDesignTimeDbContextFactory<StringManagerStorageContext>
    {
        public StringManagerStorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StringManagerStorageContext>();
            optionsBuilder.UseSqlServer("Data Source=LOCALHOST.\\SQLEXPRESS;Initial Catalog=StringManager;Integrated Security=True");
            return new StringManagerStorageContext(optionsBuilder.Options);
        }
    }
}
