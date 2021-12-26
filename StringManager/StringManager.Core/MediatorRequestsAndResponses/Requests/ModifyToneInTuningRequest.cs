using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyToneInTuningRequest : RequestBase<ToneInTuning>
    {
        public int Id { get; set; }

        public int? ToneId { get; set; }

        public int? TuningId { get; set; }

        public int? Position { get; set; }
    }
}
