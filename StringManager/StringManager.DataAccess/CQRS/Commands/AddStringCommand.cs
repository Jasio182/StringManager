using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddStringCommand : CommandBase<String, String>
    {
        public override async Task<String> Execute(StringManagerStorageContext context)
        {
            await context.Strings.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
