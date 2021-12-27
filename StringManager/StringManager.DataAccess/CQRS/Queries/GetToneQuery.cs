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
            try
            {
                return await context.Tones.FirstAsync(tone => tone.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
