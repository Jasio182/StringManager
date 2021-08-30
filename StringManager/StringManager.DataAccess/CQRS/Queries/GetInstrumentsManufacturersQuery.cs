using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    class GetInstrumentsManufacturersQuery : QueryBase<List<Manufacturer>>
    {
        public override async Task<List<Manufacturer>> Execute(StringManagerStorageContext context)
        {
            var manufacturers = await context.Manufacturers
                .Where(manufacturer=>manufacturer.Instruments != null)
                .ToListAsync();
            return manufacturers;
        }
    }
}
