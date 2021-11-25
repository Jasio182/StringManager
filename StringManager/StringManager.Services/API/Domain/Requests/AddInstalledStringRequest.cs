using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddInstalledStringRequest : RequestBase<InstalledString>
    {
        public int Position { get; set; }

        public int MyInstrumentId { get; set; }

        public int StringId { get; set; }

        public int ToneId { get; set; }
    }
}
