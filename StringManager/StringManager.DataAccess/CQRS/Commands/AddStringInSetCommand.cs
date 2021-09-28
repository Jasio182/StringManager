using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddStringInSetCommand : CommandBase<StringInSet, StringInSet>
    {
        public override async Task<StringInSet> Execute(StringManagerStorageContext context)
        {
            await context.StringsInSets.AddAsync(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
