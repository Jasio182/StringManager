using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetStringsSetQuery : QueryBase<StringsSet>
    {
        public int Id { get; set; }

        public override async Task<StringsSet> Execute(StringManagerStorageContext context)
        {
            var stringsSet = await context.StringsSets
                .Include(stringsSet => stringsSet.StringsInSet)
                .ThenInclude(stringInSet => stringInSet.String)
                .ThenInclude(strings => strings.Manufacturer)
                .FirstOrDefaultAsync(stringsSet=>stringsSet.Id==Id);
            return stringsSet;
        }
    }
}
