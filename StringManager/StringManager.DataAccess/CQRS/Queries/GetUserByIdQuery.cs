
using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetUserByIdQuery : QueryBase<User>
    {
        public int Id { get; set; }
        public override async Task<User> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.Users.FirstAsync(user => user.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
