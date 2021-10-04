using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveInstalledStringCommand : CommandBase<int, InstalledString>
    {
        public override async Task<InstalledString> Execute(StringManagerStorageContext context)
        {
            var installedStringToDelete = await context.InstalledStrings.FirstOrDefaultAsync(installedString => installedString.Id == Parameter);
            return installedStringToDelete == null ? null : await Remove(context, installedStringToDelete);
        }
        private async Task<InstalledString> Remove(StringManagerStorageContext context, InstalledString installedStringToDelete)
        {
            context.InstalledStrings.Remove(installedStringToDelete);
            await context.SaveChangesAsync();
            context.Entry(installedStringToDelete).State = EntityState.Detached;
            return installedStringToDelete;
        }
    }
}
