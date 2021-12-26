using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyTuningRequest : RequestBase<Tuning>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? NumberOfStrings { get; set; }
    }
}
