using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetToneInTuningQuery : QueryBase<ToneInTuning>
    {
        public int Id { get; set; }

        public override async Task<ToneInTuning> Execute(StringManagerStorageContext context)
        {
            var toneInTuning = await context.TonesInTunings
                .Include(toneInTuning => toneInTuning.Tone)
                .FirstOrDefaultAsync(toneInTuning => toneInTuning.Id == Id);
            return toneInTuning;
        }
    }
}
