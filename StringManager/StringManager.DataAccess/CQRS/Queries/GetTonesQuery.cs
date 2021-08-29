using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetTonesQuery : QueryBase<List<Tone>>
    {
        public override async Task<List<Tone>> Execute(StringManagerStorageContext context)
        {
            var tones = await context.Tones.ToListAsync();
            return tones;
        }
    }
}
