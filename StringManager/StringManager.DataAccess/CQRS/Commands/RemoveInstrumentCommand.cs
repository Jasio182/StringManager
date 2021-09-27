using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveInstrumentCommand : CommandBase<int, Instrument>
    {
        public override async Task<Instrument> Execute(StringManagerStorageContext context)
        {
            var instrumentToDelete = await context.Instruments.FirstOrDefaultAsync(instrument => instrument.Id == Parameter);
            return instrumentToDelete == null ? null : await Remove(context, instrumentToDelete);
        }
        private async Task<Instrument> Remove(StringManagerStorageContext context, Instrument instrumentToDelete)
        {
            context.Instruments.Remove(instrumentToDelete);
            await context.SaveChangesAsync();
            context.Entry(instrumentToDelete).State = EntityState.Detached;
            return instrumentToDelete;
        }
    }
}
