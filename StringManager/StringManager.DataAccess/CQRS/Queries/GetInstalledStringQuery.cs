using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetInstalledStringQuery : QueryBase<InstalledString>
    {
        public int Id { get; set; }

        public override async Task<InstalledString> Execute(StringManagerStorageContext context)
        {
            var installedString = await context.InstalledStrings
                .Include(installedString=> installedString.MyInstrument)
                .FirstOrDefaultAsync(installedString => installedString.Id == Id);
            return installedString;
        }
    }
}
