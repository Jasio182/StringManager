using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class AddTuningRequest : RequestBase<Tuning>
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
