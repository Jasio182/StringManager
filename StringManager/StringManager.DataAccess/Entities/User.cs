using StringManager.Core.Enums;
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class User : EntityBase 
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public GuitarDailyMaintanance DailyMaintanance { get; set; }

        public PlayStyle PlayStyle { get; set; }

        public IEnumerable<MyInstrument> MyInstruments { get; set; }

        public AccountType AccountType { get; set; }
    }
}
