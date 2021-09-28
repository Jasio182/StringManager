using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveStringCommand : CommandBase<int, String>
    {
        public override async Task<String> Execute(StringManagerStorageContext context)
        {
            var stringToDelete = await context.Strings.FirstOrDefaultAsync(thisString => thisString.Id == Parameter);
            return stringToDelete == null ? null : await Remove(context, stringToDelete);
        }
        private async Task<String> Remove(StringManagerStorageContext context, String stringToDelete)
        {
            context.Strings.Remove(stringToDelete);
            await context.SaveChangesAsync();
            context.Entry(stringToDelete).State = EntityState.Detached;
            return stringToDelete;
        }
    }
}
