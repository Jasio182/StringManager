using Microsoft.EntityFrameworkCore;
using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetStringsSetsQuery : QueryBase<List<StringsSet>>
    {
        public StringType? StringType { get; set; }

        public override async Task<List<StringsSet>> Execute(StringManagerStorageContext context)
        {
            var temp = await context.StringsSets.ToListAsync();
            var stringsSets = await context.StringsSets
                .Include(stringsSet=>stringsSet.StringsInSet)
                .ThenInclude(stringInSet=>stringInSet.String)
                .ThenInclude(strings=>strings.Manufacturer)
                .Where(stringsSet=>stringsSet.StringsInSet
                    .Any(stringsSet=>stringsSet.String.StringType == StringType || StringType == null))
                .ToListAsync();
            return stringsSets;
        }
    }
}
