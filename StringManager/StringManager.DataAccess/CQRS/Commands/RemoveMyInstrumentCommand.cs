using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveMyInstrumentCommand : CommandBase<int, MyInstrument>
    {
        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            var myInstrumentToDelete = await context.MyInstruments.FirstOrDefaultAsync(myInstrument => myInstrument.Id == Parameter);
            return myInstrumentToDelete == null ? null : await Remove(context, myInstrumentToDelete);
        }
        private async Task<MyInstrument> Remove(StringManagerStorageContext context, MyInstrument myInstrumentToDelete)
        {
            context.MyInstruments.Remove(myInstrumentToDelete);
            await context.SaveChangesAsync();
            context.Entry(myInstrumentToDelete).State = EntityState.Detached;
            return myInstrumentToDelete;
        }
    }
}
