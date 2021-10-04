using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetUsersRequest : RequestBase
    {
        public AccountType? Type { get; set; }
    }
}
