using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetStringsQuery : QueryBase<List<String>>
    {
        public override async Task<List<String>> Execute(StringManagerStorageContext context)
        {
            var strings = await context.Strings
                .Include(thisString => thisString.Manufacturer)
                .ToListAsync();
            return strings;
        }
    }
}
