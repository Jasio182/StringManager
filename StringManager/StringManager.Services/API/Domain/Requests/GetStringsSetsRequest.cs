using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetsRequest : RequestBase
    {
        public StringType StringType { get; set; }
    }
}
