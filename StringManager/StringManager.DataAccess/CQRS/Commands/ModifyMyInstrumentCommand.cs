using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    class ModifyMyInstrumentCommand : CommandBase<MyInstrument, MyInstrument>
    {
        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            context.MyInstruments.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
