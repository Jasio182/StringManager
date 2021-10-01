using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyToneInTuningRequest : RequestBase<ModifyToneInTuningResponse>
    {
        public int Id { get; set; }

        public int? ToneId { get; set; }

        public int? TuningId { get; set; }

        public int? Position { get; set; }
    }
}
