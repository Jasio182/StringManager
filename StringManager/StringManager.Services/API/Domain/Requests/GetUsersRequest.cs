using StringManager.Core.Enums;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetUsersRequest : RequestBase<GetUsersResponse>
    {
        public AccountType? Type { get; set; }
    }
}
