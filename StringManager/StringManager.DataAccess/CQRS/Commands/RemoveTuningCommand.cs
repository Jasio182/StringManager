using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveTuningCommand : CommandBase<int, Tuning>
    {
        public override async Task<Tuning> Execute(StringManagerStorageContext context)
        {
            var tuningToDelete = await context.Tunings.FirstOrDefaultAsync(tuning => tuning.Id == Parameter);
            return tuningToDelete == null ? null : await Remove(context, tuningToDelete);
        }
        private async Task<Tuning> Remove(StringManagerStorageContext context, Tuning tuningToDelete)
        {
            context.Tunings.Remove(tuningToDelete);
            await context.SaveChangesAsync();
            context.Entry(tuningToDelete).State = EntityState.Detached;
            return tuningToDelete;
        }
    }
}
