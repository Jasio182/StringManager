using Microsoft.EntityFrameworkCore;
using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetInstalledStringQuery : QueryBase<InstalledString>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public AccountType AccountType { get; set; }

        public override async Task<InstalledString> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.InstalledStrings
                .Include(installedString => installedString.Tone)
                .Include(installedString => installedString.String)
                .Include(installedString => installedString.MyInstrument)
                .ThenInclude(myInstrument => myInstrument.User)
                .FirstAsync(installedString => installedString.Id == Id
                    && (installedString.MyInstrument.User.Id == UserId || AccountType == AccountType.Admin));
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
