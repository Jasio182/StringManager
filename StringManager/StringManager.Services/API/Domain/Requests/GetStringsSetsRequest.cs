using StringManager.Core.Enums;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetsRequest : RequestBase<List<StringsSet>>
    {
        public StringType StringType { get; set; }
    }
}
