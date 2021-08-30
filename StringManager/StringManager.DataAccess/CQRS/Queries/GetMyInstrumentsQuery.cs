using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    class GetMyInstrumentsQuery : QueryBase<List<MyInstrument>>
    {
        public int UserId { get; set; }

        public override async Task<List<MyInstrument>> Execute(StringManagerStorageContext context)
        {
            var myInstruments = await context.MyInstruments
                .Include(myInstrument=>myInstrument.Instrument)
                .ThenInclude(instrument=>instrument.Manufacturer)
                .Include(myInstrument => myInstrument.User)
                .Where(myInstrument=>myInstrument.User.Id == UserId).ToListAsync();
            return myInstruments;
        }
    }
}
