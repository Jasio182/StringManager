using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyInstrumentCommand : CommandBase<Instrument, Instrument>
    {
        public override async Task<Instrument> Execute(StringManagerStorageContext context)
        {
            context.Instruments.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
