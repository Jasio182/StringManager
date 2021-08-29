using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddInstrumentCommand : CommandBase<Instrument, Instrument>
    {
        public override async Task<Instrument> Execute(StringManagerStorageContext context)
        {
            await context.Instruments.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
