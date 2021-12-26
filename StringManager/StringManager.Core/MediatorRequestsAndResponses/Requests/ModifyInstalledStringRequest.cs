using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyInstalledStringRequest : RequestBase<InstalledString>
    {
        public int Id { get; set; }

        public int? StringId { get; set; }

        public int? ToneId { get; set; }
    }
}
