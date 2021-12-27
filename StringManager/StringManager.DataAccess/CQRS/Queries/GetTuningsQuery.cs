using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetTuningsQuery : QueryBase<List<Tuning>>
    {
        public int? NumberOfStrings { get; set; }

        public override async Task<List<Tuning>> Execute(StringManagerStorageContext context)
        {
            var tuning = await context.Tunings
                .Where(tuning=>tuning.NumberOfStrings == NumberOfStrings || NumberOfStrings == null)
                .ToListAsync();
            return tuning;
        }
    }
}
