using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetMyInstrumentQuery : QueryBase<MyInstrument>
    {
        public int Id { get; set; }

        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            var myInstrument = await context.MyInstruments
                .Include(myInstrument => myInstrument.User)
                .Include(myInstrument => myInstrument.Instrument)
                .ThenInclude(instrument => instrument.Manufacturer)
                .Include(myInstrument => myInstrument.InstalledStrings)
                .ThenInclude(installedString => installedString.String)
                .Include(myInstrument => myInstrument.InstalledStrings)
                .ThenInclude(installedString => installedString.Tone)
                .FirstOrDefaultAsync(myInstrument => myInstrument.Id == Id);
            return myInstrument;
        }
    }
}
