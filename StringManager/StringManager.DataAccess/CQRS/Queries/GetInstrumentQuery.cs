using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetInstrumentQuery : QueryBase<Instrument>
    {
        public int Id { get; set; }

        public override async Task<Instrument> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.Instruments
                    .Include(instrument => instrument.Manufacturer)
                    .FirstAsync(instrument => instrument.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
