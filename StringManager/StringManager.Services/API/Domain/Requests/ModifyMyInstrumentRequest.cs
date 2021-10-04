using StringManager.Core.Enums;
using System;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyMyInstrumentRequest : RequestBase
    {
        public int Id { get; set; }

        public string OwnName { get; set; }

        public int? HoursPlayedWeekly { get; set; }

        public WhereGuitarKept? GuitarPlace { get; set; }

        public DateTime? LastDeepCleaning { get; set; }

        public DateTime? LastStringChange { get; set; }
    }
}
