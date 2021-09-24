using Microsoft.EntityFrameworkCore;
using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetUsersQuery : QueryBase<List<User>>
    {
        public AccountType? Type { get; set; }

        public override async Task<List<User>> Execute(StringManagerStorageContext context)
        {
            var users = await context.Users
                .Where(user => user.AccountType == Type || Type == null)
                .ToListAsync();
            return users;
        }
    }
}
