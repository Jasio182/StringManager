using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetUserByUsernameQuery : QueryBase<User>
    {
        public string Username { get; set; }
        public override async Task<User> Execute(StringManagerStorageContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Username == Username);
            return user;
        }
    }
}
