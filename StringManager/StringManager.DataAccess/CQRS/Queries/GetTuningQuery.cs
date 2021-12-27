using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetTuningQuery : QueryBase<Tuning>
    {
        public int Id { get; set; }

        public override async Task<Tuning> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.Tunings
                .Include(tuning => tuning.TonesInTuning)
                .ThenInclude(toneInTuning => toneInTuning.Tone)
                .FirstAsync(tuning => tuning.Id == Id);
            }
            catch(System.InvalidOperationException e)
            {
                return null;
            }
            
        }
    }
}
