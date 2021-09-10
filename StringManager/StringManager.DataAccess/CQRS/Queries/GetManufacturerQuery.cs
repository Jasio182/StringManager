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
            var manufacturer = await context.Manufacturers
                .FirstOrDefaultAsync(manufacturer=>manufacturer.Id == Id);
            return manufacturer;
        }
    }
}
