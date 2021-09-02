using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetToneQuery : QueryBase<Tone>
    {
        public int Id { get; set; }

        public override async Task<Tone> Execute(StringManagerStorageContext context)
        {
            var tone = await context.Tones.FirstOrDefaultAsync(tone=>tone.Id == Id);
            return tone;
        }
    }
}
