using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddMyInstrumentCommand : CommandBase<MyInstrument, MyInstrument>
    {
        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            await context.MyInstruments.AddAsync(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
