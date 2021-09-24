using StringManager.Core.Enums;

namespace StringManager.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public GuitarDailyMaintanance DailyMaintanance { get; set; }

        public PlayStyle PlayStyle { get; set; }

        public AccountType AccountType { get; set; }
    }
}
