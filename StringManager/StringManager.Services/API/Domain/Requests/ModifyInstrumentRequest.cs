using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyInstrumentRequest : RequestBase<ModifyInstrumentResponse>
    {
        public int Id { get; set; }

        public int? ManufacturerId { get; set; }

        public string Model { get; set; }

        public int? NumberOfStrings { get; set; }

        public int? ScaleLenghtBass { get; set; }

        public int? ScaleLenghtTreble { get; set; }
    }
}
