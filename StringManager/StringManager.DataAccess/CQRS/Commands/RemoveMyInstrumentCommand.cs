using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveMyInstrumentCommand : CommandBase<System.Tuple<int, int, Core.Enums.AccountType>, MyInstrument>
    {
        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            var myInstrumentToDelete = await context.MyInstruments.Include(myInstrument=>myInstrument.User)
                .FirstOrDefaultAsync(myInstrument => myInstrument.Id == Parameter.Item1 && (myInstrument.User.Id == Parameter.Item2
                || Parameter.Item3 == Core.Enums.AccountType.Admin));
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
