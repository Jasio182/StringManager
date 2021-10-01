using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyToneInTuningCommand : CommandBase<ToneInTuning, ToneInTuning>
    {
        public override async Task<ToneInTuning> Execute(StringManagerStorageContext context)
        {
            context.TonesInTunings.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
