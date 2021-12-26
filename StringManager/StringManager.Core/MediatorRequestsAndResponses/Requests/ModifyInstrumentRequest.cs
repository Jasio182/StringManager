using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyInstrumentRequest : RequestBase<Instrument>
    {
        public int Id { get; set; }

        public int? ManufacturerId { get; set; }

        public string Model { get; set; }

        public int? NumberOfStrings { get; set; }

        public int? ScaleLenghtBass { get; set; }

        public int? ScaleLenghtTreble { get; set; }
    }
}
