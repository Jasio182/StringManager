using StringManager.Core.Enums;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetUsersRequest : RequestBase<List<User>>
    {
        public AccountType? Type { get; set; }
    }
}
