using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class AddToneInTuningRequest : RequestBase<ToneInTuning>
    {
        public int ToneId { get; set; }

        public int TuningId { get; set; }

        public int Position { get; set; }
    }
}
