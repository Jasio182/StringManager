using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    class GetInstrumentsQuery : QueryBase<List<Instrument>>
    {
        public override async Task<List<Instrument>> Execute(StringManagerStorageContext context)
        {
            var instruments = await context.Instruments
                .Include(instrument=>instrument.Manufacturer)
                .ToListAsync();
            return instruments;
        }
    }
}
