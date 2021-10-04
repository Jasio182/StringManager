using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddUserRequest : RequestBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public GuitarDailyMaintanance DailyMaintanance { get; set; }

        public PlayStyle PlayStyle { get; set; }

        public AccountType? AccountTypeToAdd { get; set; }
    }
}
