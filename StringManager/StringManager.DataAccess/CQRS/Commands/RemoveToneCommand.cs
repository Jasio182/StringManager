using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveToneCommand : CommandBase<int, Tone>
    {
        public override async Task<Tone> Execute(StringManagerStorageContext context)
        {
            var toneToDelete = await context.Tones.FirstOrDefaultAsync(stingsSet => stingsSet.Id == Parameter);
            return toneToDelete == null ? null : await Remove(context, toneToDelete);
        }
        private async Task<Tone> Remove(StringManagerStorageContext context, Tone toneToDelete)
        {
            context.Tones.Remove(toneToDelete);
            await context.SaveChangesAsync();
            context.Entry(toneToDelete).State = EntityState.Detached;
            return toneToDelete;
        }
    }
}
