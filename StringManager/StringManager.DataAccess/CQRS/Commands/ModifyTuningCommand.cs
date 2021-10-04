using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyTuningCommand : CommandBase<Tuning, Tuning>
    {
        public override async Task<Tuning> Execute(StringManagerStorageContext context)
        {
            context.Tunings.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
