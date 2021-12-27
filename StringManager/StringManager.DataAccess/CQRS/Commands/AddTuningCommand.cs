using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddTuningCommand : CommandBase<Tuning, Tuning>
    {
        public override async Task<Tuning> Execute(StringManagerStorageContext context)
        {
            await context.Tunings.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
