using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddInstalledStringCommand : CommandBase<InstalledString,InstalledString>
    {
        public override async Task<InstalledString> Execute(StringManagerStorageContext context)
        {
            await context.InstalledStrings.AddAsync(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
