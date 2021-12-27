using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddUserCommand : CommandBase<User, User>
    {
        public override async Task<User> Execute(StringManagerStorageContext context)
        {
            await context.Users.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
