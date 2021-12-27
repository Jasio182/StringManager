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
            try
            {
                return await context.Users.FirstAsync(user => user.Username == Username);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
}
    }
}
