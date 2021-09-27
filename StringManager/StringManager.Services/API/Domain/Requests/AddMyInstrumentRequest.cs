using StringManager.Core.Enums;
using StringManager.Services.API.Domain.Responses;
using System;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddMyInstrumentRequest : RequestBase<AddMyInstrumentResponse>
    {
        public int InstrumentId { get; set; }

        public string OwnName { get; set; }

        public int HoursPlayedWeekly { get; set; }

        public WhereGuitarKept GuitarPlace { get; set; }

        public DateTime LastDeepCleaning { get; set; }

        public DateTime LastStringChange { get; set; }
    }
}
