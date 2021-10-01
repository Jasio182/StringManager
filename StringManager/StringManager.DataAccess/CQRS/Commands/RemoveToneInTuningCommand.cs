using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveToneInTuningCommand : CommandBase<int, ToneInTuning>
    {
        public override async Task<ToneInTuning> Execute(StringManagerStorageContext context)
        {
            var toneInTuningToDelete = await context.TonesInTunings.FirstOrDefaultAsync(stingsSet => stingsSet.Id == Parameter);
            return toneInTuningToDelete == null ? null : await Remove(context, toneInTuningToDelete);
        }
        private async Task<ToneInTuning> Remove(StringManagerStorageContext context, ToneInTuning toneInTuningToDelete)
        {
            context.TonesInTunings.Remove(toneInTuningToDelete);
            await context.SaveChangesAsync();
            context.Entry(toneInTuningToDelete).State = EntityState.Detached;
            return toneInTuningToDelete;
        }
    }
}
