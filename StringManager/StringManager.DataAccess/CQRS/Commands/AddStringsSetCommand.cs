using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddStringsSetCommand : CommandBase<StringsSet, StringsSet>
    {
        public override async Task<StringsSet> Execute(StringManagerStorageContext context)
        {
            await context.StringsSets.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
