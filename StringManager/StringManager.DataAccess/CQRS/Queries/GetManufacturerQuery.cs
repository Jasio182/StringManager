using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetManufacturerQuery : QueryBase<Manufacturer>
    {
        public int Id { get; set; }

        public override async Task<Manufacturer> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.Manufacturers
                    .Include(manufacturer => manufacturer.Strings)
                    .Include(manufacturer => manufacturer.Instruments)
                    .FirstAsync(manufacturer => manufacturer.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
