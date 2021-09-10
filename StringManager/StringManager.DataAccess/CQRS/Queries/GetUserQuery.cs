
using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetUserQuery : QueryBase<User>
    {
        public int Id { get; set; }
        public override async Task<User> Execute(StringManagerStorageContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == Id);
            return user;
        }
    }
}
