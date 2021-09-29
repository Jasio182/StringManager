using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddToneCommand : CommandBase<Tone, Tone>
    {
        public override async Task<Tone> Execute(StringManagerStorageContext context)
        {
            await context.Tones.AddAsync(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
