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
            try
            {
                return await context.StringsSets
                    .Include(stringsSet => stringsSet.StringsInSet)
                    .ThenInclude(stringInSet => stringInSet.String)
                    .ThenInclude(strings => strings.Manufacturer)
                    .FirstAsync(stringsSet => stringsSet.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
