using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveStringsSetCommand : CommandBase<int, StringsSet>
    {
        public override async Task<StringsSet> Execute(StringManagerStorageContext context)
        {
            var stingsSetToDelete = await context.StringsSets.FirstOrDefaultAsync(stingsSet => stingsSet.Id == Parameter);
            return stingsSetToDelete == null ? null : await Remove(context, stingsSetToDelete);
        }
        private async Task<StringsSet> Remove(StringManagerStorageContext context, StringsSet stingsSetToDelete)
        {
            context.StringsSets.Remove(stingsSetToDelete);
            await context.SaveChangesAsync();
            context.Entry(stingsSetToDelete).State = EntityState.Detached;
            return stingsSetToDelete;
        }
    }
}
