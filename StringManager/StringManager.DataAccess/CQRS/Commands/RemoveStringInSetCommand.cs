using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveStringInSetCommand : CommandBase<int, StringInSet>
    {
        public override async Task<StringInSet> Execute(StringManagerStorageContext context)
        {
            var stringInSetToDelete = await context.StringsInSets.FirstOrDefaultAsync(stringInSet => stringInSet.Id == Parameter);
            return stringInSetToDelete == null ? null : await Remove(context, stringInSetToDelete);
        }
        private async Task<StringInSet> Remove(StringManagerStorageContext context, StringInSet stringInSetToDelete)
        {
            context.StringsInSets.Remove(stringInSetToDelete);
            await context.SaveChangesAsync();
            context.Entry(stringInSetToDelete).State = EntityState.Detached;
            return stringInSetToDelete;
        }
    }
}
