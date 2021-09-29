using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyStringsSetCommand : CommandBase<StringsSet, StringsSet>
    {
        public override async Task<StringsSet> Execute(StringManagerStorageContext context)
        {
            context.StringsSets.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
