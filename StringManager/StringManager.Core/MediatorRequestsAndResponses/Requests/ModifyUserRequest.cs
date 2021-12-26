using StringManager.Core.Enums;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyUserRequest : RequestBase<User>
    {
        public int? Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public GuitarDailyMaintanance? DailyMaintanance { get; set; }

        public PlayStyle? PlayStyle { get; set; }

        public AccountType? AccountTypeToUpdate { get; set; }
    }
}
