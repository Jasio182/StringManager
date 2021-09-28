using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetStringInSetQuery : QueryBase<StringInSet>
    {
        public int Id { get; set; }

        public override async Task<StringInSet> Execute(StringManagerStorageContext context)
        {
            var stringInSet = await context.StringsInSets
                .Include(stringInSet => stringInSet.String)
                .Include(stringInSet => stringInSet.StringsSet)
                .FirstOrDefaultAsync(stringInSet => stringInSet.Id == Id);
            return stringInSet;
        }
    }
}
