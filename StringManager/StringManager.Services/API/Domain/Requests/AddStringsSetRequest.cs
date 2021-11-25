using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringsSetRequest : RequestBase<StringsSet>
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
