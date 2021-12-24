using StringManager.DataAccess;

namespace StringManager.Tests.IntegrationTests.Setup
{
    internal class OnMemoryDatabaseHandling
    {
        public static void InitializeDbForTests(StringManagerStorageContext context)
        {
            context.Manufacturers.Add(new DataAccess.Entities.Manufacturer()
            {
                Id = 1,
                Name = "testManufacturer"
            });
            context.Strings.Add(new DataAccess.Entities.String()
            {
                Id = 1,
                ManufacturerId = 1,
                Size = 52,
                SpecificWeight = 0.1,
                StringType = Core.Enums.StringType.PlainBrass,
                NumberOfDaysGood = 11
            });
            context.SaveChanges();
        }

        public static void DropTestDb(StringManagerStorageContext context)
        {
            context.Database.EnsureDeleted();
        }
    }
}
