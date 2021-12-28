using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetInstrumentsManufacturersQuery : QueryBase<List<Manufacturer>>
    {
        public override async Task<List<Manufacturer>> Execute(StringManagerStorageContext context)
        {
            var manufacturers = await context.Manufacturers
                  .Include(manufacturer => manufacturer.Instruments)
                .Where(manufacturer => manufacturer.Instruments.Count() != 0)
                .ToListAsync();
            return manufacturers;
        }
    }
}
