using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyStringInSetCommand : CommandBase<StringInSet, StringInSet>
    {
        public override async Task<StringInSet> Execute(StringManagerStorageContext context)
        {
            context.StringsInSets.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}
