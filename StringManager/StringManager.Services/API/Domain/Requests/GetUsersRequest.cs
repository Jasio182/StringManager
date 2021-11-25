using StringManager.Core.Enums;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetUsersRequest : RequestBase<List<User>>
    {
        public AccountType? Type { get; set; }
    }
}
